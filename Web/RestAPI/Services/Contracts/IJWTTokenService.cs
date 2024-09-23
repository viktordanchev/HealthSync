namespace RestAPI.Services.Contracts
{
    public interface IJWTTokenService
    {
        string GenerateRefreshToken(string userId);
        Task<string> GenerateAccessTokenAsync(string userId);
        DateTime GetTokenExpireTime(string token);
        void AppendTokenToCookie(HttpContext context, string type, string token, DateTime expTime);
    }
}
