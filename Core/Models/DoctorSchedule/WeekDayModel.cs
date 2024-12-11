namespace Core.Models.DoctorSchedule
{
    public class WeekDayModel
    {
        public DayOfWeek Day { get; set; }

        public TimeSpan WorkDayStart { get; set; }

        public TimeSpan WorkDayEnd { get; set; }
    }
}
