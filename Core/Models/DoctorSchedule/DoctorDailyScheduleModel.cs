namespace Core.Models.DoctorSchedule
{
    public class DoctorDailyScheduleModel
    {
        public DoctorDailyScheduleModel()
        {
            Meetings = new List<TimeOnly>();
        }

        public WorkDayModel WorkDay { get; set; }
        public IEnumerable<TimeOnly> Meetings { get; set; }
    }
}
