namespace Core.Models.DoctorSchedule
{
    public class WorkDayModel
    {
        public TimeOnly WorkDayStart { get; set; }
        public TimeOnly WorkDayEnd { get; set; }
        public int MeetingTimeMinutes { get; set; }
        public DayOfWeek WeekDay { get; set; }
    }
}
