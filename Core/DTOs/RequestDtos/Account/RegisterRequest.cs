using System.ComponentModel.DataAnnotations;
using static Common.Constants.User;

namespace Core.DTOs.RequestDtos.Account
{
    public class RegisterRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string VrfCode { get; set; } = null!;

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string LastName { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
