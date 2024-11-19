namespace Core.Models.Doctor
{
    public class MonthSheduleModel
    {
        public MonthSheduleModel()
        {
            AllMeetings = new List<DateTime>();
            WeekDays = new List<WeekDayModel>();
        }

        public int MeetingTimeMinutes { get; set; }

        public IEnumerable<DateTime> AllMeetings { get; set; }

        public IEnumerable<WeekDayModel> WeekDays { get; set; }
    }
}
