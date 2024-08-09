using System.ComponentModel.DataAnnotations;
using static Common.Errors;

namespace Core.Models.Account
{
    public class UserLogin
    {
        [Required(ErrorMessage = RequiredField)]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = RequiredField)]
        public string Password { get; set; } = null!;

        public bool RememberMe { get; set; }
    }
}
