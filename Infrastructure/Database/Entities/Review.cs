using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Common.Constants.Reviews;

namespace Infrastructure.Database.Entities
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [ForeignKey(nameof(DoctorId))]
        public Doctor Doctor { get; set; } = null!;

        [Required]
        [Range(RatingRangeMin, RatingRangeMax)]
        public int Rating { get; set; }

        [Required]
        public DateTime DateAndTime { get; set; }

        [Required]
        public string Reviewer { get; set; } = null!;

        [MaxLength(CommentMaxLength)]
        public string? Comment { get; set; }
    }
}
