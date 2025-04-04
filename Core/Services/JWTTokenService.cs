﻿using Core.Interfaces.Service;
using Core.Models.Account;
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
        public string GenerateRefreshToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenConfig.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var expireTime = _cookiesConfig.RefreshJWTTokenMonths;

            var claims = new List<Claim>()
            {
                new Claim("JwtId", Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _jwtTokenConfig.Issuer,
                audience: _jwtTokenConfig.Audience,
                claims: claims,
                expires: DateTime.Now.AddMonths(_cookiesConfig.RefreshJWTTokenMonths),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Generate JWT access token.
        /// </summary>
        public string GenerateAccessToken(UserClaimsModel userClaims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenConfig.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var expireTime = _cookiesConfig.AccessJWTTokenMinutes;

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, userClaims.Id),
                new Claim(ClaimTypes.Email, userClaims.Email),
                new Claim(ClaimTypes.Name, userClaims.Name)
            };

            foreach (var role in userClaims.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                issuer: _jwtTokenConfig.Issuer,
                audience: _jwtTokenConfig.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_cookiesConfig.AccessJWTTokenMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
