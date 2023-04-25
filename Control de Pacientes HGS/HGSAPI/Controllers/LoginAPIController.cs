using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HGSAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginAPIController : Controller
    {
        private readonly IConfiguration configuration;

        public LoginAPIController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        [Route("Login")]
        [HttpPost]
        public HGSModel.Token LoginAPILogin(HGSModel.Token tokenRequest)
        {
            HGSModel.Token tokenResult = new();

            if (tokenRequest._token == "AUF){whU8:nUvg6=ce4k5y=qGed(#&")
            {
                string applcationName = "CPAPI";
                tokenResult.ExpirationTime = DateTime.Now.AddMinutes(30);
                tokenResult._token = CustomTokenJWT(applcationName, tokenResult.ExpirationTime);
            }

            return tokenResult;
        }

        private string CustomTokenJWT(string ApplicationName, DateTime token_expiration)
        {
            var _symmetricSecurityKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"])
                );

            var _signingCredentials = new SigningCredentials(
                    _symmetricSecurityKey, SecurityAlgorithms.HmacSha256
                );

            var _Header = new JwtHeader(_signingCredentials);

            var _Claims = new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, ApplicationName),
                new Claim("Name", "nombrepersona")
            };

            var _Payload = new JwtPayload(
                    issuer: configuration["JWT:Issuer"],
                    audience: configuration["JWT:Audience"],
                    claims: _Claims,
                    notBefore: DateTime.Now,
                    expires: token_expiration
                );

            var _Token = new JwtSecurityToken(
                    _Header,
                    _Payload
                );

            return new JwtSecurityTokenHandler().WriteToken(_Token);
        }
    }
}