namespace Core.Models.DoctorSchedule
{
    public class DailyScheduleModel
    {
        public DailyScheduleModel()
        {
            Meetings = new List<TimeOnly>();
        }

        public WorkDayModel WorkDay { get; set; }
        public IEnumerable<TimeOnly> Meetings { get; set; }
    }
}
