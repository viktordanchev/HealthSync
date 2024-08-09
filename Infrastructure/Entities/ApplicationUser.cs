using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static Common.Validations.User;

namespace Infrastructure.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        [MaxLength(UNCLength)]
        public string UCN { get; set; } = null!;
    }
}
