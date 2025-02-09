using Core.Models.Account;

namespace Core.Interfaces.Service
{
    public interface IJwtTokenService
    {
        string GenerateRefreshToken();
        string GenerateAccessToken(UserClaimsModel userClaims);
    }
}
