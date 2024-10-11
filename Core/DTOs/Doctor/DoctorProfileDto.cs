namespace Core.DTOs.Doctor
{
    public class DoctorProfileDto
    {
        public DoctorProfileDto()
        {
            Reviews = new List<ReviewDto>();
        }

        public string Name { get; set; } = null!;

        public string Specialty { get; set; } = null!;

        public string Hospital { get; set; } = null!;

        public double Raiting { get; set; }

        public int TotalReviews { get; set; }

        public IEnumerable<ReviewDto> Reviews { get; set; }
    }
}
