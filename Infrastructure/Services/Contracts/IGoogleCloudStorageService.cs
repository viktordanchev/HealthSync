using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services.Contracts
{
    public interface IGoogleCloudStorageService
    {
        Task<string> UploadProfileImageAsync(IFormFile file);
    }
}
