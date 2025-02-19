using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.RequestDtos.Meetings
{
    public class AddMeetingRequest
    {
        [Required]
        public int DoctorId { get; set; }

        [Required]
        public string DateAndTime { get; set; } = null!;

        [Required]
        public string PatientId { get; set; } = null!;
    }
}
