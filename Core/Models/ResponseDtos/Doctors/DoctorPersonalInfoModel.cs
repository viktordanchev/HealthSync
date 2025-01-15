namespace Core.Models.ResponseDtos.Doctors
{
    public class DoctorPersonalInfoModel
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Hospital { get; set; } = null!;

        public string Specialty { get; set; } = null!;

        public string ContactEmail { get; set; } = string.Empty;
            
        public string ContactPhoneNumber { get; set; } = string.Empty;
    }
}
