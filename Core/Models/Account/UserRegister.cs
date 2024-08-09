using System.ComponentModel.DataAnnotations;
using static Common.Errors;
using static Common.Validations.User;

namespace Server.Models.Account
{
    public class UserRegister
    {
        [Required(ErrorMessage = RequiredField)]
        [EmailAddress(ErrorMessage = InvalidEmail)]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = RequiredField)]
        [RegularExpression(NameMatch, ErrorMessage = InvalidName)]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = RequiredField)]
        [RegularExpression(NameMatch, ErrorMessage = InvalidName)]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = RequiredField)]
        [StringLength(UNCLength, ErrorMessage = InvalidUNCLength)]
        public string UCN { get; set; } = null!;

        [Phone(ErrorMessage = InvalidPhoneNumber)]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = RequiredField)]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = RequiredField)]
        [Compare("Password", ErrorMessage = RequiredField)]
        public string ConfirmPassword { get; set; } = null!;
    }
}
