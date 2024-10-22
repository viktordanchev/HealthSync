namespace Core.ResponseDtos.Doctor
{
    public class DoctorProfileDto
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string ImgUrl { get; set; } = string.Empty;

        public string Specialty { get; set; } = null!;

        public string Hospital { get; set; } = null!;

        public double Rating { get; set; }

        public int TotalReviews { get; set; }
    }
}
