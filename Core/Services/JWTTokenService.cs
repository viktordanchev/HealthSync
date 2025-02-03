using Core.Contracts.Services;
using Core.Services.Configs;
using Infrastructure.Database.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
    public class JWTTokenService : IJWTTokenService
    {
        private UserManager<ApplicationUser> _userManager;
        private JWTTokenConfig _jwtTokenConfig;
        private CookiesConfig _cookiesConfig;

        public JWTTokenService(UserManager<ApplicationUser> userManager,
            IOptions<JWTTokenConfig> jwtOptions,
            IOptions<CookiesConfig> cookiesOptions)
        {
            _userManager = userManager;
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
                new Claim("JWTId", Guid.NewGuid().ToString()),
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

            var user = await _userManager.FindByIdAsync(userId);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName)
            };

            var roles = await _userManager.GetRolesAsync(user);
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

        /// <summary>
        /// Append current JWT token to cookie.
        /// </summary>
        public void AppendRefreshTokenToCookie(HttpContext context, string token)
        {
            context.Response.Cookies.Append("refreshToken", token,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    IsEssential = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.Now.AddMinutes(5),
                });
        }
    }
}
