using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.RequestDtos.Doctors
{
    public class UpdateWeeklyScheduleRequest
    {
        [Required]
        public bool IsWorkDay { get; set; }

        [Required]
        public DayOfWeek WeekDay { get; set; }

        public TimeOnly WorkDayStart { get; set; }
        public TimeOnly WorkDayEnd { get; set; }
        public int MeetingTimeMinutes { get; set; }
    }
}
