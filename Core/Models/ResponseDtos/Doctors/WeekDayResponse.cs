namespace Core.Models.ResponseDtos.Doctors
{
    public class WeekDayResponse
    {
        public int Id { get; set; }

        public DayOfWeek WeekDay { get; set; }

        public bool IsWorkDay { get; set; }

        public TimeSpan WorkDayStart { get; set; }

        public TimeSpan WorkDayEnd { get; set; }

        public int MeetingTimeMinutes { get; set; }
    }
}
