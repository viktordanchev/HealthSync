namespace Core.Models.Doctor
{
    public class UnavailableDaysModel
    {
        public UnavailableDaysModel()
        {
            DaysOff = new List<DateTime>();
            WeeklyDaysOff = new List<DayOfWeek>();
        }

        public IEnumerable<DateTime> DaysOff { get; set; }

        public IEnumerable<DayOfWeek> WeeklyDaysOff { get; set; }
    }
}
