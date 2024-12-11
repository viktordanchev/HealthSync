namespace Core.Models.ResponseDtos.DoctorSchedule
{
    public class MonthScheduleResponse
    {
        public MonthScheduleResponse(string date, bool isAvailable)
        {
            Date = date;
            IsAvailable = isAvailable;
        }

        public string Date { get; set; } = null!;

        public bool IsAvailable { get; set; }
    }
}
