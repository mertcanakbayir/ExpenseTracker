
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Core.Security.JWT
{
    public class TokenHelper : ITokenHelper
    {
        private readonly JwtSettings _jwtSettings;

        public TokenHelper(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public AccessToken CreateToken(Guid userId, string email, string role)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
           {
               new Claim(JwtRegisteredClaimNames.Sub,userId.ToString()),
               new Claim(JwtRegisteredClaimNames.Email,email),
               new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
               new Claim("role",role.ToString())
           };

            var tokenExpiry = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiresInMinutes);
            var token = new JwtSecurityToken(
                issuer:_jwtSettings.Issuer,
                audience:_jwtSettings.Audience,
                claims:claims,
                expires:tokenExpiry,
                signingCredentials:credentials
                );

            if (string.IsNullOrEmpty(_jwtSettings.Key)) {
            throw new ArgumentNullException("JWT_KEY TANIMLI DEĞİL");
            }

            if (_jwtSettings.Key.Length < 32) { 
                throw new ArgumentException("JWT_KEY en az 32 karakter olmalı!");
            }

            return new AccessToken
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = tokenExpiry
            };

        }
        public Guid? ValidateToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.Key);

            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier);

                return userIdClaim != null ? Guid.Parse(userIdClaim.Value) : null;
            }
            catch
            {
                return null;
            }
        }


    }
}
