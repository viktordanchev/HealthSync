using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.RequestDtos.Doctors
{
    public class UpdateWeeklyScheduleRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public bool IsWorkDay { get; set; }

        [Required]
        public TimeOnly WorkDayStart { get; set; }

        [Required]
        public TimeOnly WorkDayEnd { get; set; }

        [Required]
        public int MeetingTimeMinutes { get; set; }
    }
}
