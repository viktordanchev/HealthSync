using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static Common.Constants.User;

namespace Infrastructure.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Meetings = new List<Meeting>();
        }

        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string LastName { get; set; } = null!;

        public IEnumerable<Meeting> Meetings { get; set; }
    }
}
