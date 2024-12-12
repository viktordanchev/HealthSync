using System.ComponentModel.DataAnnotations;

namespace RestAPI.Dtos.RequestDtos.Doctors
{
    public class GetAvailableMeetingHours
    {
        [Required]
        public int DoctorId { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
