namespace Core.DTOs.ResponseDtos.Meetings
{
    public class DailyMeetingResponse
    {
        public int Id { get; set; }
        public DateTime DateAndTime { get; set; }
        public string PatientId { get; set; } = null!;
        public string PatientName { get; set; } = null!;
        public string? PatientPhoneNumber { get; set; }
    }
}
