using Core.DTOs.RequestDtos.Account;
using Core.DTOs.ResponseDtos.Account;
using Core.Interfaces.Repository;
using Core.Models.Account;
using Infrastructure.Database.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public UserRepository(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> IsUserExistAsync(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);

            return user != null;
        }

        public async Task AddUserAsync(RegisterRequest requestData)
        {
            var newUser = new ApplicationUser()
            {
                UserName = requestData.Email,
                Email = requestData.Email,
                FirstName = requestData.FirstName,
                LastName = requestData.LastName,
                EmailConfirmed = true
            };

            await _userManager.CreateAsync(newUser, requestData.Password);
        }

        public async Task<bool> IsUserLoggedInAsync(LoginRequest requestData)
        {
            var result = await _signInManager
                .PasswordSignInAsync(requestData.Email, requestData.Password, false, lockoutOnFailure: false);

            return result.Succeeded;
        }

        public async Task<UserClaimsModel> GetUserClaimsAsync(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            var userRoles = await _userManager.GetRolesAsync(user);

            var userData = new UserClaimsModel() 
            {
                Id = user.Id,
                Email = user.Email,
                Name = $"{user.FirstName} {user.LastName}",
                Roles = userRoles
            };

            return userData;
        }

        public async Task UpdateUserPassword(RecoverPasswordRequest requestData, string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);

            await _userManager.ResetPasswordAsync(user, requestData.Token, requestData.Password);
        }

        public async Task<UserDataResponse> GetUserDataAsync(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);

            var userData = new UserDataResponse()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };

            return userData;
        }

        public async Task<string> GeneratePasswordResetTokenAsync(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);

            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task ResetPasswordAsync(RecoverPasswordRequest requestData, string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);

            await _userManager.ResetPasswordAsync(user, requestData.Token, requestData.Password);
        }

        public async Task UpdateUserDataAsync(UpdateUserRequest requestData, string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);

            await _userManager.ChangePasswordAsync(user, requestData.CurrentPassword, requestData.NewPassword);
            user.FirstName = requestData.FirstName;
            user.LastName = requestData.LastName;
            user.PhoneNumber = requestData.PhoneNumber;

            await _userManager.UpdateAsync(user);
        }
    }
}
