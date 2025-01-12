namespace Core.Models.ResponseDtos.DoctorSchedule
{
    public class MonthScheduleResponse
    {
        public MonthScheduleResponse(DateTime date, bool isAvailable)
        {
            Date = date;
            IsAvailable = isAvailable;
        }

        public DateTime Date { get; set; }

        public bool IsAvailable { get; set; }
    }
}
