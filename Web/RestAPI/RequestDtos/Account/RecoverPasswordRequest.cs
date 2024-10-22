using System.ComponentModel.DataAnnotations;
using static Common.Errors;
using static Common.Errors.Account;

namespace RestAPI.RequestDtos.Account
{
    public class RecoverPasswordRequest
    {
        [Required]
        public string Token { get; set; } = null!;

        [Required(ErrorMessage = $"Password {RequiredField}")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = $"Confirm password {RequiredField}")]
        [Compare("Password", ErrorMessage = PasswordMatch)]
        public string ConfirmPassword { get; set; } = null!;
    }
}
