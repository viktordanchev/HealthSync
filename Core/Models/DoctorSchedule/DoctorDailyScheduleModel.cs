namespace Core.Models.DoctorSchedule
{
    public class DoctorDailyScheduleModel
    {
        public DoctorDailyScheduleModel()
        {
            Meetings = new List<TimeSpan>();
        }

        public WorkDayModel WorkDay { get; set; }
        public IEnumerable<TimeSpan> Meetings { get; set; }
    }
}
