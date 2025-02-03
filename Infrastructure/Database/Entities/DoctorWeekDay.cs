using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Database.Entities
{
    public class DoctorWeekDay
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [ForeignKey(nameof(DoctorId))]
        public Doctor Doctor { get; set; } = null!;

        [Required]
        public DayOfWeek WeekDay { get; set; }

        [Required]
        public bool IsWorkDay { get; set; }

        public TimeSpan Start { get; set; }

        public TimeSpan End { get; set; }

        public int MeetingTimeMinutes { get; set; }
    }
}
