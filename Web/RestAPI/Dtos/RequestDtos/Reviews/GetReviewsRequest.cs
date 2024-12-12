using System.ComponentModel.DataAnnotations;

namespace RestAPI.Dtos.RequestDtos.Reviews
{
    public class GetReviewsRequest
    {
        [Required]
        public int DoctorId { get; set; }

        public int Index { get; set; }
    }
}
