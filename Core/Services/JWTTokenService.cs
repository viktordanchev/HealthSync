using Core.Interfaces.Service;
using Core.Services.Configs;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestAPI.Services
{
    /// <summary>
    /// This class is responsible for Jwt token.
    /// </summary>
    public class JwtTokenService : IJwtTokenService
    {
        private JwtTokenConfig _jwtTokenConfig;
        private CookiesConfig _cookiesConfig;

        public JwtTokenService(IOptions<JwtTokenConfig> jwtOptions,
            IOptions<CookiesConfig> cookiesOptions)
        {
            _jwtTokenConfig = jwtOptions.Value;
            _cookiesConfig = cookiesOptions.Value;
        }

        /// <summary>
        /// Generate JWT refresh token.
        /// </summary>
        public string GenerateRefreshToken(string userId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenConfig.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var expireTime = _cookiesConfig.RefreshJWTTokenMonths;

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim("JwtId", Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _jwtTokenConfig.Issuer,
                audience: _jwtTokenConfig.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Generate JWT access token.
        /// </summary>
        public async Task<string> GenerateAccessTokenAsync(string userId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenConfig.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var expireTime = _cookiesConfig.AccessJWTTokenMinutes;

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                issuer: _jwtTokenConfig.Issuer,
                audience: _jwtTokenConfig.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
