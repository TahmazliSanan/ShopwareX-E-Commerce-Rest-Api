using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShopwareX.Auth;
using ShopwareX.Entities;
using ShopwareX.Services.Abstracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShopwareX.Services.Concretes
{
    public class JwtGenerator(IOptions<JwtSettings> options) : IJwtGenerator
    {
        private readonly JwtSettings _jwtSettings = options.Value;

        public string GenerateJwt(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new("Id", user.Id.ToString()),
                new("FullName", user.FullName),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.DateOfBirth, user.DateOfBirth?.ToString() ?? ""),
                new(ClaimTypes.Gender, user.Gender?.Name ?? ""),
                new(ClaimTypes.Role, user.Role?.Name ?? "User")
            };

            var jwt = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpireMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
