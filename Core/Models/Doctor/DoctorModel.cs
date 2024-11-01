using Infrastructure.Entities;

namespace Core.Models.Doctor
{
    class DoctorModel
    {
        public DoctorModel()
        {
            Meetings = new List<TimeSpan>();
        }

        public int MeetingTimeMinutes { get; set; }

        public TimeSpan WorkDayStart { get; set; }

        public TimeSpan WorkDayEnd { get; set; }

        public IEnumerable<TimeSpan> Meetings { get; set; }
    }
}
