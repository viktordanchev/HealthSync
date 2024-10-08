using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities
{
    class Doctor
    {
        [Required]
        public string IdenitityId { get; set; } = null!;

        [ForeignKey(nameof(IdenitityId))]
        public ApplicationUser Identity { get; set; } = null!;

        [Required]
        public string HospitalId { get; set; } = null!;

        [ForeignKey(nameof(HospitalId))]
        public Hospital Hospital { get; set; } = null!;
    }
}
