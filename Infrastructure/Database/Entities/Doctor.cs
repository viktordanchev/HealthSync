using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Common.Constants.Doctors;

namespace Infrastructure.Database.Entities
{
    public class Doctor
    {
        public Doctor()
        {
            Reviews = new List<DoctorReview>();
            Meetings = new List<DoctorMeeting>();
            WorkWeek = new List<DoctorWeekDay>();
            DaysOff = new List<DoctorDayOff>();
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
        public DoctorSpecialty Specialty { get; set; } = null!;

        public string? ContactEmail { get; set; }

        public string? ContactPhoneNumber { get; set; }

        public string? ImgUrl { get; set; }

        [MaxLength(InformationMaxLength)]
        public string? Information { get; set; }

        public IEnumerable<DoctorReview> Reviews { get; set; }
        public IEnumerable<DoctorMeeting> Meetings { get; set; }
        public IEnumerable<DoctorWeekDay> WorkWeek { get; set; }
        public IEnumerable<DoctorDayOff> DaysOff { get; set; }
    }
}
