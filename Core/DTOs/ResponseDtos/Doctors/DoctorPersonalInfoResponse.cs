namespace Core.DTOs.ResponseDtos.Doctors
{
    public class DoctorPersonalInfoResponse
    {
        public DoctorPersonalInfoResponse()
        {
            WeeklySchedule = new List<WeekDayResponse>();
        }

        public string Name { get; set; } = null!;

        public string? ImgUrl { get; set; }

        public int HospitalId { get; set; }

        public string Hospital { get; set; } = null!;

        public int SpecialtyId { get; set; }

        public string Specialty { get; set; } = null!;

        public string? PersonalInformation { get; set; }

        public string? ContactEmail { get; set; }

        public string? ContactPhoneNumber { get; set; }

        public IEnumerable<WeekDayResponse> WeeklySchedule { get; set; }
    }
}
