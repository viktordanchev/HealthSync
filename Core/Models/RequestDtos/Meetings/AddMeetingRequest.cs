using System.ComponentModel.DataAnnotations;

namespace RestAPI.Dtos.RequestDtos.Meetings
{
    public class AddMeetingRequest
    {
        [Required]
        public int DoctorId { get; set; }

        [Required]
        public DateTime DateAndTime { get; set; }

        [Required]
        public string PatientId { get; set; } = null!;
    }
}
