using Common.EntityConstants;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [RegularExpression(ApplicationUserConstants.NameMatch)]
        public string FirstName { get; set; } = null!;

        [Required]
        [RegularExpression(ApplicationUserConstants.NameMatch)]
        public string LastName { get; set; } = null!;

        [Required]
        [MinLength(ApplicationUserConstants.UNCLength)]
        [MaxLength(ApplicationUserConstants.UNCLength)]
        public int UCN { get; set; }
    }
}
