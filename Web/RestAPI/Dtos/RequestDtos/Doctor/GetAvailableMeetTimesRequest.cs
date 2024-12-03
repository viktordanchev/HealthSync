using System.ComponentModel.DataAnnotations;

namespace RestAPI.Dtos.RequestDtos.Doctor
{
    public class GetAvailableMeetTimesRequest
    {
        [Required]
        public int DoctorId { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
