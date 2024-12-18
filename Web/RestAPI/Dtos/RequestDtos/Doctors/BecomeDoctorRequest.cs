using System.ComponentModel.DataAnnotations;

namespace RestAPI.Dtos.RequestDtos.Doctors
{
    public class BecomeDoctorRequest
    {
        [EmailAddress]
        public string ContactEmail { get; set; } = string.Empty;

        [Phone]
        public string ContactPhoneNumber { get; set; } = string.Empty;

        [Required]
        public int HospitalId { get; set; }

        [Required]
        public int SpecialtyId { get; set; }
    }
}
