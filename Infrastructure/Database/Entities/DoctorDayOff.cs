using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Database.Entities
{
    public class DoctorDayOff
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [ForeignKey(nameof(DoctorId))]
        public Doctor Doctor { get; set; } = null!;

        [Required]
        public int Month { get; set; }

        [Required]
        public int Day { get; set; }

        [Required]
        public bool isWorkDay { get; set; }

        public TimeOnly WorkDayStart { get; set; }

        public TimeOnly WorkDayEnd { get; set; }

        public int MeetingTimeMinutes { get; set; }
    }
}
