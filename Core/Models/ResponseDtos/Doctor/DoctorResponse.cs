namespace Core.Models.ResponseDtos.Doctor
{
    public class DoctorResponse
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string ImgUrl { get; set; } = string.Empty;

        public string Specialty { get; set; } = null!;

        public double Rating { get; set; }

        public int TotalReviews { get; set; }
    }
}
