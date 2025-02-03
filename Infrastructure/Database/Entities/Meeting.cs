using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Database.Entities
{
    public class Meeting
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [ForeignKey(nameof(DoctorId))]
        public Doctor Doctor { get; set; } = null!;

        [Required]
        public string PatientId { get; set; } = null!;

        [ForeignKey(nameof(PatientId))]
        public ApplicationUser Patient { get; set; } = null!;

        [Required]
        public DateTime DateAndTime { get; set; }
    }
}
