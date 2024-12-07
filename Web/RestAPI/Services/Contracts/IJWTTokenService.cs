namespace RestAPI.Services.Contracts
{
    public interface IJWTTokenService
    {
        string GenerateRefreshToken(string userId);
        Task<string> GenerateAccessTokenAsync(string userId);
        void AppendRefreshTokenToCookie(HttpContext context, string token);
    }
}
