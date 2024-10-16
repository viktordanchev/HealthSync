using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities
{
    public class Review
    {
        [Key]
        public string Id { get; set; } = null!;

        [Required]
        public string DoctorId { get; set; } = null!;

        [ForeignKey(nameof(DoctorId))]
        public Doctor Doctor { get; set; } = null!;

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Reviewer { get; set; } = null!;
    }
}
