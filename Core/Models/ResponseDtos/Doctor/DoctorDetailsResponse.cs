namespace Core.Models.ResponseDtos.Doctor
{
    public class DoctorDetailsResponse
    {
        public string HospitalName { get; set; } = null!;

        public string HospitalAddress { get; set; } = null!;

        public string Information { get; set; } = string.Empty;

        public string PhoneNumber {  get; set; } = string.Empty;
    }
}
