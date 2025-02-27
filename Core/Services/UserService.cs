using Core.DTOs.RequestDtos.Account;
using Core.DTOs.ResponseDtos.Account;
using Core.Interfaces.Repository;
using Core.Interfaces.Service;
using Core.Models.Account;

namespace Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<bool> AddUserAsync(RegisterRequest requestData)
        {
            return await _userRepo.AddUserAsync(requestData);
        }

        public async Task<bool> IsUserLoginDataValidAsync(LoginRequest requestData)
        {
            return await _userRepo.IsUserLoginDataValidAsync(requestData);
        }

        public async Task<UserClaimsModel> GetUserClaimsAsync(string userEmail)
        {
            return await _userRepo.GetUserClaimsAsync(userEmail);
        }

        public async Task<UserDataResponse> GetUserDataAsync(string userEmail)
        {
            return await _userRepo.GetUserDataAsync(userEmail);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(string userEmail)
        {
            return await _userRepo.GeneratePasswordResetTokenAsync(userEmail);
        }

        public async Task ResetPasswordAsync(RecoverPasswordRequest requestData, string userEmail)
        {
            await _userRepo.ResetPasswordAsync(requestData, userEmail);
        }

        public async Task UpdateUserDataAsync(UpdateUserRequest requestData, string userEmail)
        {
            await _userRepo.UpdateUserDataAsync(requestData, userEmail);
        }
    }
}
