using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities
{
    public class Specialty
    {
        public Specialty()
        {
            Doctors = new List<Doctor>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Type { get; set; } = null!;

        public IEnumerable<Doctor> Doctors { get; set; }
    }
}
