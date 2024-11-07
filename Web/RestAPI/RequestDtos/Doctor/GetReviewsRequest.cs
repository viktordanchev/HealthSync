using System.ComponentModel.DataAnnotations;

namespace RestAPI.RequestDtos.Doctor
{
    public class GetReviewsRequest
    {
        [Required]
        public int DoctorId { get; set; }

        public int Index { get; set; }
    }
}
