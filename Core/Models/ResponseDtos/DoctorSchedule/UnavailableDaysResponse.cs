namespace Core.Models.ResponseDtos.DoctorSchedule
{
    public class UnavailableDaysResponse
    {
        public UnavailableDaysResponse()
        {
            DaysOff = new List<DateTime>();
            WeeklyDaysOff = new List<DayOfWeek>();
        }

        public IEnumerable<DateTime> DaysOff { get; set; }

        public IEnumerable<DayOfWeek> WeeklyDaysOff { get; set; }
    }
}
