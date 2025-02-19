namespace Core.DTOs.ResponseDtos.Meetings
{
    public class DoctorMeetingResponse
    {
        public int Id { get; set; }
        public DateTime DateAndTime { get; set; }
        public string PatientName { get; set; } = null!;
        public string? PatientPhoneNumber { get; set; }
    }
}
