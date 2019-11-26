using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace PersonalBlog.App.Services
{
    using System.IdentityModel.Tokens.Jwt;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using PersonalBlog.App.Settings;

    public class SignInService : AuthenticationStateProvider,IAuthService
    {
        private readonly ILocalStorageService  _localStorageService;
        private readonly IJsonWebTokenService _jsonWebTokenService;
        private readonly ILogger<SignInService> _logger;
        private readonly SystemSetting _systemSetting;


        public SignInService(
            ISettingManager settingManager,
            ILocalStorageService localStorageService,
            IJsonWebTokenService jsonWebTokenService,
            ILogger<SignInService> logger
            )
        {
            this._localStorageService = localStorageService;
            this._jsonWebTokenService = jsonWebTokenService;
            this._logger = logger;
            this._systemSetting = settingManager.GetSystemSetting();
        }

       internal const string STORAGE_KEY = "Auth_Token";

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorageService.GetItemAsync<string>(STORAGE_KEY);
            if (string.IsNullOrWhiteSpace(token))
            {
                return new AuthenticationState(new ClaimsPrincipal());
            }

            var principal = new ClaimsPrincipal();

            try
            {
                var handler = new JwtSecurityTokenHandler();

                principal = handler.ValidateToken(token, new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_systemSetting.AppSecret)),
                    ValidIssuer = "STS",
                    ValidAudience = _systemSetting.UserName
                },out SecurityToken securityToken);
            }catch(Exception ex)
            {
                _logger.LogError(ex, "Validate Token Failed");
                return new AuthenticationState(new ClaimsPrincipal());
            }

            var state= new AuthenticationState(principal);
            return state;
        }

        public async Task<SignInResult> PasswordSignInAsync(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException(nameof(userName));
            }

            if (password is null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            if(!_systemSetting.UserName.Equals(userName) && !_systemSetting.Password.Equals(password))
            {
                return SignInResult.Failed;
            }

            try
            {
                var token = _jsonWebTokenService.GetJsonWebToken();




                await _localStorageService.SetItemAsync(STORAGE_KEY, token);
                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal())));
                return SignInResult.Success;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return SignInResult.Failed;
            }

        }

        public async Task SignOutAsync()
        {
            await _localStorageService.RemoveItemAsync(STORAGE_KEY);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal())));
        }
    }
}
