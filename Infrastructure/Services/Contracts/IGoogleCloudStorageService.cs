using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services
{
    public interface IGoogleCloudStorageService
    {
        Task<string> UploadProfileImageAsync(IFormFile file);
    }
}
