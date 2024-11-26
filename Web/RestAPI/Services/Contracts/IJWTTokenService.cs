namespace RestAPI.Services.Contracts
{
    public interface IJWTTokenService
    {
        string GenerateRefreshToken(string userId);
        Task<string> GenerateAccessTokenAsync(string userId);
        DateTime GetTokenExpireTime(string token);
        void AppendRefreshTokenToCookie(HttpContext context, string token, DateTime expTime);
    }
}
