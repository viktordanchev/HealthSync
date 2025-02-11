namespace Core.Models.DoctorSchedule
{
    public class MonthlyDaysOffModel
    {
        public MonthlyDaysOffModel()
        {
            DaysOff = new List<DateTime>();
            WorkWeekDaysOff = new List<DayOfWeek>();
        }

        public IEnumerable<DateTime> DaysOff { get; set; }
        public IEnumerable<DayOfWeek> WorkWeekDaysOff { get; set; }
    }
}
