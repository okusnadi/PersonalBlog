using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace PersonalBlog.App.Services
{
    public class JsonWebTokenService : IJsonWebTokenService
    {
        private readonly BlogSettings _options;

        public JsonWebTokenService(IOptions<BlogSettings> options)
        {
            this._options = options.Value;
        }

        public string GetJsonWebToken()
        {
               var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(new SecurityTokenDescriptor
            {
                Audience = _options.UserName,
                Issuer = "STS",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.AppSecret)), SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(new Claim[]
                  {
                     new Claim(ClaimTypes.Name,_options.UserName),
                  }, "jwt", ClaimTypes.Name, ClaimTypes.Role),                 
            });

            return handler.WriteToken(token);
        }
    }
}
