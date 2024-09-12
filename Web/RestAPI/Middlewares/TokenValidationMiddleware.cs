using Core.Services.Contracts;

namespace RestAPI.Middlewares
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private ITokenService _tokenService;

        public TokenValidationMiddleware(RequestDelegate next, IConfiguration configuration, ITokenService tokenService)
        {
            _next = next;
            _configuration = configuration;
            _tokenService = tokenService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var accessToken = context.Request.Cookies["accessToken"];
            var refreshToken = context.Request.Cookies["refreshToken"];

            if (accessToken == null && refreshToken != null)
            {
                var claims = context.User.Claims;

                var newToken = _tokenService.GenerateAccessToken(claims);

                context.Response.Cookies.Append("accessToken", newToken,
                new CookieOptions
                {
                    Expires = DateTime.Now.AddMinutes(1),
                    HttpOnly = true,
                    Secure = true,
                    IsEssential = true,
                    SameSite = SameSiteMode.None,
                });
            }

            await _next(context);
        }
    }
}
