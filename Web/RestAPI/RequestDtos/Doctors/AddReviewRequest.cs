using System.ComponentModel.DataAnnotations;
using static Common.Errors;
using static Common.Errors.Doctors;

namespace RestAPI.RequestDtos.Doctors
{
    public class AddReviewRequest
    {
        [Required(ErrorMessage = $"DoctorId {RequiredField}")]
        public string DoctorId { get; set; } = null!;

        [Required(ErrorMessage = $"Rating {RequiredField}")]
        [Range(1, 5, ErrorMessage = InvalidRating)]
        public int Rating { get; set; }

        [Required(ErrorMessage = $"Reviewer {RequiredField}")]
        public string Reviewer { get; set; } = null!;
    }
}
