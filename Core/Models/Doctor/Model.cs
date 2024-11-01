namespace Core.Models.Doctor
{
    public class Model
    {
        public Model()
        {
            DaysOff = new List<DateTime>();
            WeeklyDaysOff = new List<DayOfWeek>();
        }

        public IEnumerable<DateTime> DaysOff { get; set; }

        public IEnumerable<DayOfWeek> WeeklyDaysOff { get; set; }
    }
}
