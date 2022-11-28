using EntrenatePues.Core.Domain;
using EntrenatePues.Core.Interfaces.Services.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EntrenatePues.Core.Services.Auth
{
    public class JwtFactory : IJwtFactory
    {
        private readonly IConfiguration _configuration;

        public JwtFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Token GenerateToken(User user)
        {
            Claim[] claims = new[]
            {
                 new Claim("UserName", user.FullName),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                 new Claim("Id", user.Id.ToString()),
            };


            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:SecretKey"]));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                _configuration["Tokens:Issuer"],
                _configuration["Tokens:Audience"],
                claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);

            return new Token(new JwtSecurityTokenHandler().WriteToken(token), token.ValidTo.ToLocalTime());
        }
    }
}
