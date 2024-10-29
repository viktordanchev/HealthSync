using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities
{
    public class Doctor
    {
        public Doctor()
        {
            Reviews = new List<Review>();
            Meetings = new List<Meeting>();
            WorkDays = new List<WorkDay>();
            DaysOff = new List<DayOff>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string IdentityId { get; set; } = null!;

        [ForeignKey(nameof(IdentityId))]
        public ApplicationUser Identity { get; set; } = null!;

        [Required]
        public int HospitalId { get; set; }

        [ForeignKey(nameof(HospitalId))]
        public Hospital Hospital { get; set; } = null!;

        [Required]
        public int SpecialtyId { get; set; }

        [ForeignKey(nameof(SpecialtyId))]
        public Specialty Specialty { get; set; } = null!;

        [Required]
        public int MeetingTime { get; set; }

        public string? ImgUrl { get; set; } 

        public IEnumerable<Review> Reviews { get; set; }
        public IEnumerable<Meeting> Meetings { get; set; }
        public IEnumerable<WorkDay> WorkDays { get; set; }
        public IEnumerable<DayOff> DaysOff { get; set; }
    }
}
