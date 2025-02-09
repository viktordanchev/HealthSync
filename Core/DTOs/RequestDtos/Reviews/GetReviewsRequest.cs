using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.RequestDtos.Reviews
{
    public class GetReviewsRequest
    {
        [Required]
        public int DoctorId { get; set; }

        public int Index { get; set; }
    }
}
