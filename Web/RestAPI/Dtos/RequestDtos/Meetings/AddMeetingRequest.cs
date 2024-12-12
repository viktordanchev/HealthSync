using System.ComponentModel.DataAnnotations;

namespace RestAPI.Dtos.RequestDtos.Meetings
{
    public class AddMeetingRequest
    {
        [Required]
        public int DoctorId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string UserId { get; set; } = null!;
    }
}
