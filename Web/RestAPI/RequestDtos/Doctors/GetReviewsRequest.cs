using System.ComponentModel.DataAnnotations;
using static Common.Errors;

namespace RestAPI.RequestDtos.Doctors
{
    public class GetReviewsRequest
    {
        [Required(ErrorMessage = $"DoctorId {RequiredField}")]
        public string DoctorId { get; set; } = null!;

        public int Index { get; set; }
    }
}
