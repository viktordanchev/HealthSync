using Infrastructure.Entities;
using System.Security.Claims;

namespace Core.Services.Contracts
{
    public interface ITokenService
    {
        string GenerateRefreshToken(IEnumerable<Claim> claims);
        string GenerateAccessToken(IEnumerable<Claim> claims);
    }
}
