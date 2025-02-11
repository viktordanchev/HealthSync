using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Database.Entities
{
    public class DoctorSpecialty
    {
        public DoctorSpecialty()
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
