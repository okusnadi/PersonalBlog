using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using PersonalBlog.App.Settings;

namespace PersonalBlog.App.Services
{
    public class JsonWebTokenService : IJsonWebTokenService
    {
        private readonly SystemSetting _systemSetting;

        public JsonWebTokenService(ISettingManager settingManager)
        {
            this._systemSetting = settingManager.GetSystemSetting();
        }

        public string GetJsonWebToken()
        {
               var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(new SecurityTokenDescriptor
            {
                Audience = _systemSetting.UserName,
                Issuer = "STS",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_systemSetting.AppSecret)), SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(new Claim[]
                  {
                     new Claim(ClaimTypes.Name,_systemSetting.UserName),
                  }, "jwt", ClaimTypes.Name, ClaimTypes.Role),                 
            });

            return handler.WriteToken(token);
        }
    }
}
