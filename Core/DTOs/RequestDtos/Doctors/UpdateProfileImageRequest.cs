using Microsoft.AspNetCore.Http;

namespace Core.DTOs.RequestDtos.Doctors
{
    public class UpdateProfileImageRequest
    {
        public string? OldProfileImageUrl { get; set; }

        public IFormFile? ProfileImage { get; set; }
    }
}
