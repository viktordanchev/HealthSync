namespace Core.Models.DoctorSchedule
{
    public class MonthlyBusyDaysModel
    {
        public MonthlyBusyDaysModel()
        {
            AllMeetings = new List<DateTime>();
            WeekDays = new List<WorkDayModel>();
        }

        public IEnumerable<DateTime> AllMeetings { get; set; }
        public IEnumerable<WorkDayModel> WeekDays { get; set; }
    }
}
