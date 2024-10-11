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
        public string Id { get; set; } = null!;

        [Required]
        public string Type { get; set; } = null!;

        public IEnumerable<Doctor> Doctors { get; set; }
    }
}
