using RestAPI.Services.Contracts;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
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
        private IConfiguration _config;

        public JWTTokenService(UserManager<ApplicationUser> userManager,
            IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        /// <summary>
        /// Generate JWT refresh token.
        /// </summary>
        public string GenerateRefreshToken(string userId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var expireTime = int.Parse(_config["CookieSettings:RefreshJWTTokenMonths"]);

            var claims = new List<Claim>()
            {
                new Claim("Identifier", userId),
                new Claim("JWTId", Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
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
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var expireTime = int.Parse(_config["CookieSettings:AccessJWTTokenMinutes"]);

            var userData = await _userManager.FindByIdAsync(userId);

            var claims = new List<Claim>()
            {
                new Claim("Identifier", userId),
                new Claim("Name", userData.FirstName + " " + userData.LastName),
                new Claim("Email", userData.Email),
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Receive JWT token expiration time.
        /// </summary>
        public DateTime GetTokenExpireTime(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            return jwtToken.ValidTo;
        }

        /// <summary>
        /// Append current JWT token to cookie.
        /// </summary>
        public void AppendTokenToCookie(HttpContext context, string type, string token, DateTime expTime)
        {
            context.Response.Cookies.Append(type, token,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    IsEssential = true,
                    SameSite = SameSiteMode.None,
                    Expires = expTime,
                });
        }
    }
}
