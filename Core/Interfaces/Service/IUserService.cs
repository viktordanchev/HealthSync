using Core.DTOs.RequestDtos.Account;
using Core.Models.Account;

namespace Core.Interfaces.Service
{
    public interface IUserService
    {
        Task<bool> IsUserExistByEmailAsync(string userEmail);
        Task AddUserAsync(RegisterRequest requestData);
        Task<bool> IsUserLoggedInAsync(LoginRequest requestData);
        Task<UserDataModel> GetUserDataAsync(string userEmail);
        Task<string> GeneratePasswordResetTokenAsync(string userEmail);
        Task ResetPasswordAsync(RecoverPasswordRequest requestData, string userEmail);
        Task UpdateUserDataAsync(UpdateUserRequest requestData, string userId);
    }
}
