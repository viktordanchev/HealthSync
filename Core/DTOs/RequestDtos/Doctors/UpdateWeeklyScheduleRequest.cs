using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.RequestDtos.Doctors
{
    public class UpdateWeeklyScheduleRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public bool IsWorkDay { get; set; }

        public TimeOnly WorkDayStart { get; set; }
        public TimeOnly WorkDayEnd { get; set; }
        public int MeetingTimeMinutes { get; set; }
    }
}
