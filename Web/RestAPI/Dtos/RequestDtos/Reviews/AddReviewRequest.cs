using System.ComponentModel.DataAnnotations;
using static Common.Constants.Reviews;

namespace RestAPI.Dtos.RequestDtos.Reviews
{
    public class AddReviewRequest
    {
        [Required]
        public int DoctorId { get; set; }

        [Required]
        [Range(RatingRangeMin, RatingRangeMax)]
        public int Rating { get; set; }

        [MaxLength(CommentMaxLength)]
        public string Comment { get; set; } = string.Empty;
    }
}
