using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using MyoX.Application.Abstraction;
using MyoX.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyoX.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        public TokenService(IConfiguration config)
        {
            _config = config;
        }
        public string GenerateJWT(UserEntity user)
        {
            string secretKey = _config["Authentication:secret"] ?? throw new ArgumentNullException("Authentication:secret");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

            var claims = new ClaimsIdentity([
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            ]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Audience = _config["Authentication:audience"],
                Issuer = _config["Authentication:issuer"],
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddMinutes(10)
            };

            var tokenHandler = new JsonWebTokenHandler();

            return tokenHandler.CreateToken(tokenDescriptor);
        }
    }
}
