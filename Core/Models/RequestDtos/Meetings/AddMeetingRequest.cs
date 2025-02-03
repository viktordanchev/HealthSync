using System.ComponentModel.DataAnnotations;

namespace RestAPI.Dtos.RequestDtos.Meetings
{
    public class AddMeetingRequest
    {
        [Required]
        public int DoctorId { get; set; }

        [Required]
        public string Date { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = null!;
    }
}
