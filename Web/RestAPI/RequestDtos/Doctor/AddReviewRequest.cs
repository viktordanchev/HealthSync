using System.ComponentModel.DataAnnotations;
using static Common.Errors;
using static Common.Errors.Doctor;
using static Common.Constants.Review;

namespace RestAPI.RequestDtos.Doctor
{
    public class AddReviewRequest
    {
        [Required(ErrorMessage = $"DoctorId {RequiredField}")]
        public int DoctorId { get; set; }

        [Required(ErrorMessage = $"Rating {RequiredField}")]
        [Range(RatingMin, RatingMax, ErrorMessage = InvalidRating)]
        public int Rating { get; set; }

        [MaxLength(CommentMaxLength, ErrorMessage = InvalidCommentLength)]
        public string Comment { get; set; } = string.Empty;
    }
}
