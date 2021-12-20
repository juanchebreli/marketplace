using marketplace.DTO.UserDTO;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace marketplace.Helpers
{
    public static class JwtMiddleware
    {
        public static string GenerateJWTToken(UserLoginDTO userInfo, JWT JWT, IConfiguration config)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWT.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.username),
                new Claim("usuario", userInfo.username),
                new Claim(ClaimTypes.Role,userInfo.role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var token = new JwtSecurityToken(
            issuer: config["Jwt: Issuer"],
            audience: config["Jwt: Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(8),
            signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
