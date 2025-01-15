namespace Core.Models.ResponseDtos.Doctors
{
    public class DoctorInfoModel
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Hospital { get; set; } = null!;

        public string Specialty { get; set; } = null!;

        public string ContactEmail { get; set; } = null!;

        public string ContactPhoneNumber { get; set; } = null!;
    }
}
