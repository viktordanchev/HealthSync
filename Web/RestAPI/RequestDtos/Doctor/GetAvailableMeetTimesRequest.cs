using System.ComponentModel.DataAnnotations;
using static Common.Errors;

namespace RestAPI.RequestDtos.Doctor
{
    public class GetAvailableMeetTimesRequest
    {
        [Required(ErrorMessage = $"DoctorId {RequiredField}")]
        public int DoctorId { get; set; }

        [Required(ErrorMessage = $"Date {RequiredField}")]
        public DateTime Date { get; set; }
    }
}
