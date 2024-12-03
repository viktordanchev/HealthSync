using System.ComponentModel.DataAnnotations;

namespace RestAPI.Dtos.RequestDtos.Account
{
    public class VerifyAccountRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string VrfCode { get; set; } = null!;
    }
}
