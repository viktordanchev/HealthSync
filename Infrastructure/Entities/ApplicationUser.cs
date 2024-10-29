using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Meetings = new List<Meeting>();
        }

        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        public IEnumerable<Meeting> Meetings { get; set; }
    }
}
