namespace Core.Models.ResponseDtos.Doctor
{
    public class DoctorProfileResponse
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string ImgUrl { get; set; } = string.Empty;

        public string Specialty { get; set; } = null!;

        public string Hospital { get; set; } = null!;

        public string HospitalAddress { get; set; } = null!;

        public double Rating { get; set; }

        public int TotalReviews { get; set; }
    }
}
