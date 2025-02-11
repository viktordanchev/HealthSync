namespace Core.Models.DoctorSchedule
{
    public class WorkDayModel
    {
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public int MeetingTimeMinutes { get; set; }
        public DayOfWeek WeekDay { get; set; }
    }
}
