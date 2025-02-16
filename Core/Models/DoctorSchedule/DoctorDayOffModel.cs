namespace Core.Models.DoctorSchedule
{
    public class DoctorDayOffModel
    {
        public int Month { get; set; }
        public int Day { get; set; }
        public bool isWorkDay { get; set; }
        public TimeOnly WorkDayStart { get; set; }
        public TimeOnly WorkDayEnd { get; set; }
        public int MeetingTimeMinutes { get; set; }
    }
}
