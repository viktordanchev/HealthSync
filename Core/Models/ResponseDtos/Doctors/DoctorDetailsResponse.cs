namespace Core.Models.ResponseDtos.Doctors
{
    public class DoctorDetailsResponse : DoctorResponse
    {
        public string HospitalName { get; set; } = null!;

        public string HospitalAddress { get; set; } = null!;

        public string? Information { get; set; }

        public string? ContactEmail { get; set; }

        public string? ContactPhoneNumber { get; set; }
    }
}
