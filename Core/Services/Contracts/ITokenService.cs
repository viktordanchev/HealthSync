namespace Core.Services.Contracts
{
    public interface ITokenService
    {
        string GenerateRefreshToken(string userId);
        Task<string> GenerateAccessTokenAsync(string userId);
        DateTime GetTokenExpireTime(string token);
    }
}
