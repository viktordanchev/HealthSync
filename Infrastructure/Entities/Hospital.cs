using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities
{
    public class Hospital
    {
        public Hospital()
        {
            Doctors = new List<Doctor>();
        }

        [Key]
        public string Id { get; set; } = null!;

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Address { get; set; } = null!;

        public IEnumerable<Doctor> Doctors { get; set; }
    }
}
