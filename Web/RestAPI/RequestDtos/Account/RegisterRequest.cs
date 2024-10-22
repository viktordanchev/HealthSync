using System.ComponentModel.DataAnnotations;
using static Common.Errors;
using static Common.Errors.Account;

namespace RestAPI.RequestDtos.Account
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = $"Email {RequiredField}")]
        [EmailAddress(ErrorMessage = InvalidEmail)]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = $"First name {RequiredField}")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = $"Last name {RequiredField}")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = $"Password {RequiredField}")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = $"Confirm password {RequiredField}")]
        [Compare("Password", ErrorMessage = PasswordMatch)]
        public string ConfirmPassword { get; set; } = null!;
    }
}
