using RestAPI.Services.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace RestAPI.Middlewares
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _config;

        public TokenValidationMiddleware(RequestDelegate next, 
            IConfiguration config)
        {
            _next = next;
            _config = config;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var accessToken = context.Request.Cookies["accessToken"];
            var refreshToken = context.Request.Cookies["refreshToken"];

            if (accessToken == null && refreshToken != null)
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadJwtToken(refreshToken);
                var userId = jsonToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

                var tokenService = context.RequestServices.GetRequiredService<ITokenService>();

                var newAccessToken = await tokenService.GenerateAccessTokenAsync(userId);
                var accessTokenExpireTime = tokenService.GetTokenExpireTime(newAccessToken);
                tokenService.AppendTokenToCookie(context, "accessToken", newAccessToken, accessTokenExpireTime);

                context.Request.Headers["Authorization"] = "Bearer " + newAccessToken;
            }

            await _next(context);
        }
    }
}
