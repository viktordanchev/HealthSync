using Microsoft.AspNetCore.Http;

namespace Core.Interfaces.ExternalServices
{
    public interface IGoogleCloudStorageService
    {
        Task<string> UploadProfileImageAsync(IFormFile file);
    }
}
