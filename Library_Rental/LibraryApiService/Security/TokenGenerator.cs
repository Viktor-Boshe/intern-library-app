using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryApiService.Security
{
    public class TokenGenerator
    {
        private readonly string _secret;
        private const int _remainingMinutes = 1;

        public TokenGenerator(string secret)
        {
            _secret = secret;
        }
        public string GenerateToken(Users user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.username),
                new Claim(ClaimTypes.Role, user.administrator? "admin" : "user"),
                new Claim("user_id", user.user_id.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_remainingMinutes),
            signingCredentials: creds
        );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
