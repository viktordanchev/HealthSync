using Core.DTOs.RequestDtos.Account;
using Core.DTOs.ResponseDtos.Account;
using Core.Models.Account;

namespace Core.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<bool> AddUserAsync(RegisterRequest requestData);
        Task<bool> IsUserLoginDataValidAsync(LoginRequest requestData);
        Task<UserClaimsModel> GetUserClaimsAsync(string userEmail);
        Task UpdateUserPassword(RecoverPasswordRequest requestData, string userEmail);
        Task<UserDataResponse> GetUserDataAsync(string userEmail);
        Task<string> GeneratePasswordResetTokenAsync(string userEmail);
        Task ResetPasswordAsync(RecoverPasswordRequest requestData, string userEmail);
        Task UpdateUserDataAsync(UpdateUserRequest requestData, string userEmail);
        Task<bool> IsUserExistAsync(string email);
        Task AssignUserRoleAsync(string userId, string role);
    }
}
