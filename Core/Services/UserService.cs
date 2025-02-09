using Core.DTOs.RequestDtos.Account;
using Core.Interfaces.Repository;
using Core.Interfaces.Service;
using Core.Models.Account;

namespace Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository accountRepo)
        {
            _userRepo = accountRepo;
        }

        public async Task<bool> IsUserExistByEmailAsync(string userEmail)
        {
            return await _userRepo.IsUserExistByEmailAsync(userEmail);
        }

        public async Task AddUserAsync(RegisterRequest requestData)
        {
            await _userRepo.AddUserAsync(requestData);
        }

        public async Task<bool> IsUserLoggedInAsync(LoginRequest requestData)
        {
            return await _userRepo.IsUserLoggedInAsync(requestData);
        }

        public async Task<UserDataModel> GetUserDataAsync(string userEmail)
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

        public async Task UpdateUserDataAsync(UpdateUserRequest requestData, string userId)
        {
            await _userRepo.UpdateUserDataAsync(requestData, userId);
        }
    }
}
