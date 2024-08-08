using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static Common.Validations.ApplicationUser;

namespace Infrastructure.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [RegularExpression(NameMatch)]
        public string FirstName { get; set; } = null!;

        [Required]
        [RegularExpression(NameMatch)]
        public string LastName { get; set; } = null!;

        [Required]
        
        public string UCN { get; set; } = null!;
    }
}
