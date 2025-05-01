using System.ComponentModel.DataAnnotations;
using Core.Attributes;
using static Common.Constants.Doctors;

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
        [TimeAfter(nameof(WorkDayStart))]
        public TimeOnly WorkDayEnd { get; set; }

        [Required]
        [Range(MeetingTimeMinutesMin, MeetingTimeMinutesMax)]
        public int MeetingTimeMinutes { get; set; }
    }
}
