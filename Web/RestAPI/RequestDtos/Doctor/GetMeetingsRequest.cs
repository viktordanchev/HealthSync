using System.ComponentModel.DataAnnotations;
using static Common.Errors;

namespace RestAPI.RequestDtos.Doctor
{
    public class GetMeetingsRequest
    {
        [Required(ErrorMessage = $"DoctorId {RequiredField}")]
        public int DoctorId { get; set; }

        [Required(ErrorMessage = $"DayOfWeek {RequiredField}")]
        public DayOfWeek DayOfWeek { get; set; }
    }
}
