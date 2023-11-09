using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TicketEase.Application.ServicesImplementation
{
    public class JwtTokenGeneratorService
    {
        private readonly IConfiguration _configuration;

        public JwtTokenGeneratorService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwtToken(string userId, string email)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.Email, email),
                // Add other claims as needed
            };

            var token = new JwtSecurityToken(
                _configuration["JwtSettings:ValidIssuer"],
                _configuration["JwtSettings:ValidAudience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:AccessTokenExpiration"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
