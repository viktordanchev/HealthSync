using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities
{
    public class WorkDay
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int WorkScheduleId { get; set; }

        [ForeignKey(nameof(WorkScheduleId))]
        public WorkSchedule WorkSchedule { get; set; } = null!;

        [Required]
        public DayOfWeek Day { get; set; }

        public TimeSpan Start { get; set; }

        public TimeSpan End { get; set; }

        [Required]
        public bool IsWorkingDay { get; set; }
    }
}
