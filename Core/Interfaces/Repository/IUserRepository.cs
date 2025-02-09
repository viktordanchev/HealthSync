using Core.DTOs.RequestDtos.Account;
using Core.DTOs.ResponseDtos.Account;
using Core.Models.Account;

namespace Core.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<bool> IsUserExistByEmailAsync(string userEmail);
        Task AddUserAsync(RegisterRequest requestData);
        Task<bool> IsUserLoggedInAsync(LoginRequest requestData);
        Task<UserDataModel> GetUserDataAsync(string userEmail);
        Task UpdateUserPassword(RecoverPasswordRequest requestData, string userEmail);
        Task<UserDataResponse> GetUserDataByIdAsync(string userId);
        Task<string> GeneratePasswordResetTokenAsync(string userEmail);
        Task ResetPasswordAsync(RecoverPasswordRequest requestData, string userEmail);
        Task UpdateUserDataAsync(UpdateUserRequest requestData, string userId);
    }
}
