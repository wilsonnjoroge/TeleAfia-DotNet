
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TeleAfiaPersonal.Application.Common.interfaces.Authentication;
using TeleAfiaPersonal.Domain.UserAggregate.Entity;

namespace TeleAfiaPersonal.Infrastructure.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtSettings _jwtSettings;

        public JwtTokenGenerator( IOptions<JwtSettings> jwtOptions)
        {
            _jwtSettings = jwtOptions.Value;
        }

        public string GenerateRandomToken(ApplicationUser user)
        {
            // Generate a secure 6-digit token
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] randomNumber = new byte[4]; // 4 bytes is enough to generate a large range of numbers
                rng.GetBytes(randomNumber);

                // Convert the bytes to a uint and ensure it's within the 6-digit range
                uint generatedValue = BitConverter.ToUInt32(randomNumber, 0);
                int sixDigitToken = (int)(generatedValue % 900000) + 100000; // Ensures the token is in the range 100000-999999

                return sixDigitToken.ToString("D6"); // Convert to string with leading zeroes if necessary
            }
        }

        public string GenerateToken(ApplicationUser user)
        {
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
                SecurityAlgorithms.HmacSha256
            );

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var securityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                claims: claims,
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}