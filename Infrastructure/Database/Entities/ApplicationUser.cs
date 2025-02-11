using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static Common.Constants.User;

namespace Infrastructure.Database.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Meetings = new List<DoctorMeeting>();
        }

        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string LastName { get; set; } = null!;

        public IEnumerable<DoctorMeeting> Meetings { get; set; }
    }
}
