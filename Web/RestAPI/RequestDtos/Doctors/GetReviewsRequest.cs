using System.ComponentModel.DataAnnotations;
using static Common.Errors;

namespace RestAPI.RequestDtos.Doctors
{
    public class GetReviewsRequest
    {
        [Required(ErrorMessage = $"DoctorId {RequiredField}")]
        public int DoctorId { get; set; }

        public int Index { get; set; }
    }
}
