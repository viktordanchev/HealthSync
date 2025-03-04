namespace Core.DTOs.ResponseDtos.Doctors
{
    public class DoctorResponse
    {
        public int Id { get; set; }

        public string IdentityId { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? ImgUrl { get; set; }

        public string Specialty { get; set; } = null!;

        public double Rating { get; set; }

        public int TotalReviews { get; set; }
    }
}
