using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EF_DiegoQuispeR.Services
{
    public class AuthService
    {
        public string GenerateJwt(string username, string rol)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("daangrupo1diegosohaildanieldaannumber1"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, rol)
            };

            var token = new JwtSecurityToken(
                    claims : claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
