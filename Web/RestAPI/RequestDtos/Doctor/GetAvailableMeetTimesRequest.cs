using System.ComponentModel.DataAnnotations;

namespace RestAPI.RequestDtos.Doctor
{
    public class GetAvailableMeetTimesRequest
    {
        [Required]
        public int DoctorId { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
