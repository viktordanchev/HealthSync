using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities
{
    public class DayOff
    {
        public DayOff()
        {
            Doctors = new List<Doctor>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public IEnumerable<Doctor> Doctors { get; set; }
    }
}
