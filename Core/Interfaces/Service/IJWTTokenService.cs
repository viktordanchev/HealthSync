namespace Core.Interfaces.Service
{
    public interface IJwtTokenService
    {
        string GenerateRefreshToken(string userId);
        Task<string> GenerateAccessTokenAsync(string userId);
    }
}
