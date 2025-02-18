namespace Core.Models.DoctorSchedule
{
    public class MonthlyUnavailableDaysModel
    {
        public MonthlyUnavailableDaysModel()
        {
            DaysOff = new List<DateTime>();
            BusyDays = new List<DateTime>();
            WeeklyDaysOff = new List<DayOfWeek>();
        }

        public IEnumerable<DateTime> DaysOff { get; set; }
        public IEnumerable<DateTime> BusyDays { get; set; }
        public IEnumerable<DayOfWeek> WeeklyDaysOff { get; set; }
    }
}
