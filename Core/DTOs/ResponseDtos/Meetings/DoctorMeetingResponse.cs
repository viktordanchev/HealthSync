namespace Core.DTOs.ResponseDtos.Meetings
{
    public class DoctorMeetingResponse
    {
        public DoctorMeetingResponse()
        {
            DailyMeetings = new List<DailyMeetingResponse>();
        }

        public string Date { get; set; } = null!;
        public IEnumerable<DailyMeetingResponse> DailyMeetings { get; set; }
    }
}
