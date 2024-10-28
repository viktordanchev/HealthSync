using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities
{
    public class WorkSchedule
    {
        public WorkSchedule()
        {
            Meetings = new List<Meeting>();
            WorkDays = new List<WorkDay>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [ForeignKey(nameof(DoctorId))]
        public Doctor Doctor { get; set; } = null!;

        [Required]
        public int MeetingTime { get; set; }

        public IEnumerable<Meeting> Meetings { get; set; }
        public IEnumerable<WorkDay> WorkDays { get; set; }
    }
}
