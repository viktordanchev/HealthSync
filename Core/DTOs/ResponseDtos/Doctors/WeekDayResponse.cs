namespace Core.DTOs.ResponseDtos.Doctors
{
    public class WeekDayResponse
    {
        public int Id { get; set; }

        public DayOfWeek WeekDay { get; set; }

        public bool IsWorkDay { get; set; }

        public TimeOnly WorkDayStart { get; set; }

        public TimeOnly WorkDayEnd { get; set; }

        public int MeetingTimeMinutes { get; set; }
    }
}
