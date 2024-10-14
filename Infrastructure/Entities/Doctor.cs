using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities
{
    public class Doctor
    {
        public Doctor()
        {
            Meetings = new List<Meeting>();
            Reviews = new List<Review>();
        }

        [Key]
        public string Id { get; set; } = null!;

        [Required]
        public string IdenitityId { get; set; } = null!;

        [ForeignKey(nameof(IdenitityId))]
        public ApplicationUser Identity { get; set; } = null!;

        [Required]
        public string HospitalId { get; set; } = null!;

        [ForeignKey(nameof(HospitalId))]
        public Hospital Hospital { get; set; } = null!;

        [Required]
        public string SpecialtyId { get; set; } = null!;

        [ForeignKey(nameof(SpecialtyId))]
        public Specialty Specialty { get; set; } = null!;

        public string? ImgUrl { get; set; } 

        public IEnumerable<Meeting> Meetings { get; set; } 
        public IEnumerable<Review> Reviews { get; set; } 
    }
}
