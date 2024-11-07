using System.ComponentModel.DataAnnotations;
using static Common.Errors;
using static Common.Errors.Doctor;

namespace RestAPI.RequestDtos.Doctor
{
    public class GetDaysInMonthRequest
    {
        [Required(ErrorMessage = $"DoctorId {RequiredField}")]
        public int DoctorId { get; set; }

        [Required(ErrorMessage = $"Month {RequiredField}")]
        [Range(1, 12, ErrorMessage = InvalidMonthRange)]
        public int Month { get; set; }

        [Required(ErrorMessage = $"Year {RequiredField}")]
        public int Year { get; set; }
    }
}
