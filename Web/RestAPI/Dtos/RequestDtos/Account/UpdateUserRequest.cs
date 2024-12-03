using RestAPI.Attributes;
using System.ComponentModel.DataAnnotations;

namespace RestAPI.Dtos.RequestDtos.Account
{
    public class UpdateUserRequest
    {
        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        public string PhoneNumber { get; set; } = string.Empty;

        public string CurrentPassword { get; set; } = string.Empty;

        [Validate(nameof(CurrentPassword))]
        public string NewPassword { get; set; } = string.Empty;

        [Validate(nameof(NewPassword))]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
