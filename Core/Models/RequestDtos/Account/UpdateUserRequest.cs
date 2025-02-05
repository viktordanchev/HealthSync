using Core.Attributes;
using System.ComponentModel.DataAnnotations;
using static Common.Constants.User;

namespace RestAPI.Dtos.RequestDtos.Account
{
    public class UpdateUserRequest
    {
        private string? phoneNumber;

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string LastName { get; set; } = null!;

        [Phone]
        public string? PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                phoneNumber = string.IsNullOrEmpty(value) ? null : value;
            }
        }

        public string CurrentPassword { get; set; } = string.Empty;

        [Validate(nameof(CurrentPassword))]
        public string NewPassword { get; set; } = string.Empty;

        [Validate(nameof(NewPassword))]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
