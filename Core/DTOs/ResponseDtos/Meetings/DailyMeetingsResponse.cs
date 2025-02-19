namespace Core.DTOs.ResponseDtos.Meetings
{
    public class DailyMeetingsResponse
    {
        public DailyMeetingsResponse()
        {
            DailyMeetings = new List<DoctorMeetingResponse>();
        }

        public string Date { get; set; } = null!;
        public IEnumerable<DoctorMeetingResponse> DailyMeetings { get; set; }
    }
}
