using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Database.Entities
{
    public class Hospital
    {
        public Hospital()
        {
            Doctors = new List<Doctor>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Address { get; set; } = null!;

        public IEnumerable<Doctor> Doctors { get; set; }
    }
}
