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
        public string Text { get; set; } = null!;

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Reviewer { get; set; } = null!;
    }
}
