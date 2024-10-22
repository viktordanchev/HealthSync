using System.ComponentModel.DataAnnotations;
using static Common.Errors;
using static Common.Errors.Account;

namespace RestAPI.RequestDtos.Account
{
    public class VerifyAccountRequest
    {
        [Required(ErrorMessage = $"Email {RequiredField}")]
        [EmailAddress(ErrorMessage = InvalidEmail)]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = $"Verification code {RequiredField}")]
        public string VrfCode { get; set; } = null!;
    }
}
