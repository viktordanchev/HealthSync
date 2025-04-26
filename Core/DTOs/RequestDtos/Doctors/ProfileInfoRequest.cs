using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.RequestDtos.Doctors
{
    public class ProfileInfoRequest
    {
        private string? contactEmail;
        private string? contactPhoneNumber;

        [EmailAddress]
        public string? ContactEmail
        {
            get { return contactEmail; }
            set
            {
                contactEmail = string.IsNullOrEmpty(value) ? null : value;
            }
        }

        [Phone]
        public string? ContactPhoneNumber
        {
            get { return contactPhoneNumber; }
            set
            {
                contactPhoneNumber = string.IsNullOrEmpty(value) ? null : value;
            }
        }

        [Required]
        public int HospitalId { get; set; }

        [Required]
        public int SpecialtyId { get; set; }

        public string? ProfileImage { get; set; }

        public string? PersonalInformation { get; set; }
    }
}
