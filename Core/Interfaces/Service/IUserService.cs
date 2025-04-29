using Core.DTOs.RequestDtos.Account;
using Core.DTOs.ResponseDtos.Account;
using Core.Models.Account;

namespace Core.Interfaces.Service
{
    public interface IUserService
    {
        Task<bool> AddUserAsync(RegisterRequest requestData);
        Task<bool> IsUserLoginDataValidAsync(LoginRequest requestData);
        Task<UserClaimsModel> GetUserClaimsAsync(string userEmail);
        Task<UserDataResponse> GetUserDataAsync(string userEmail);
        Task<string> GeneratePasswordResetTokenAsync(string userEmail);
        Task ResetPasswordAsync(RecoverPasswordRequest requestData, string userEmail);
        Task UpdateUserDataAsync(UpdateUserRequest requestData, string userEmail);
        Task<bool> IsUserExistAsync(string email);
    }
}
