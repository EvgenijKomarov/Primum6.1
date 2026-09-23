using CoreConnection.DTOs;
using Microsoft.IdentityModel.Tokens;
using PrimumWebAPI.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PrimumWebAPI.Services
{
    public class JwtTokenService(JwtSettings settings)
    {

        public string GenerateToken(UserDto user)
        {
            var securityKey = settings.Seed;
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Surname, user.Surname)
            };

            var token = new JwtSecurityToken(
                issuer: settings.Issuer,
                audience: settings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(settings.ExpirationMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
