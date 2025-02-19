namespace Core.DTOs.ResponseDtos.Meetings
{
    public class DoctorMeetingInfoResponse
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? ImgUrl { get; set; }

        public string Hospital { get; set; } = null!;

        public string HospitalAddress { get; set; } = null!;

        public DateTime DateAndTime { get; set; }
    }
}
