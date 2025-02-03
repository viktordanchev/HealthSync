using Microsoft.AspNetCore.Http;

namespace Core.Contracts.Services
{
    public interface IJWTTokenService
    {
        string GenerateRefreshToken(string userId);
        Task<string> GenerateAccessTokenAsync(string userId);
        void AppendRefreshTokenToCookie(HttpContext context, string token);
    }
}
