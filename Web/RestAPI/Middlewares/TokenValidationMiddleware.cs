using Core.Services.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RestAPI.Middlewares
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public TokenValidationMiddleware(RequestDelegate next, 
            IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
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

                context.Response.Cookies.Append("accessToken", newAccessToken,
                new CookieOptions
                {
                    Expires = DateTime.Now.AddMinutes(1),
                    HttpOnly = true,
                    Secure = true,
                    IsEssential = true,
                    SameSite = SameSiteMode.None,
                });

                context.Request.Headers["Authorization"] = "Bearer " + newAccessToken;
            }

            await _next(context);
        }
    }
}
