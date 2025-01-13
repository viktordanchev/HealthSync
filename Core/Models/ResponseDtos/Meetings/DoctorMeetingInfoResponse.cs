using Microsoft.VisualBasic;

namespace Core.Models.ResponseDtos.Meetings
{
    public class DoctorMeetingInfoResponse
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string ImgUrl { get; set; } = string.Empty;

        public string Hospital { get; set; } = null!;

        public string HospitalAddress { get; set; } = null!;

        public DateTime DateAndTime { get; set; }
    }
}
