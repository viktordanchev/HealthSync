using Core.DTOs.RequestDtos.Account;
using Core.Interfaces.Repository;
using Core.Interfaces.Service;
using Core.Models.Account;

namespace Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepo;

        public AccountService(IAccountRepository accountRepo)
        {
            _accountRepo = accountRepo;
        }

        public async Task<bool> IsUserExistByEmailAsync(string userEmail)
        {
            return await _accountRepo.IsUserExistByEmailAsync(userEmail);
        }

        public async Task AddUserAsync(RegisterRequest requestData)
        {
            await _accountRepo.AddUserAsync(requestData);
        }

        public async Task<bool> IsUserLoggedInAsync(LoginRequest requestData)
        {
            return await _accountRepo.IsUserLoggedInAsync(requestData);
        }

        public async Task<UserDataModel> GetUserDataAsync(string userEmail)
        {
            return await _accountRepo.GetUserDataAsync(userEmail);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(string userEmail)
        {
            return await _accountRepo.GeneratePasswordResetTokenAsync(userEmail);
        }

        public async Task ResetPasswordAsync(RecoverPasswordRequest requestData, string userEmail)
        {
            await _accountRepo.ResetPasswordAsync(requestData, userEmail);
        }

        public async Task UpdateUserDataAsync(UpdateUserRequest requestData, string userId)
        {
            await _accountRepo.UpdateUserDataAsync(requestData, userId);
        }
    }
}
