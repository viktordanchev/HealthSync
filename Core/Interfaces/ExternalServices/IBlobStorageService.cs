using Microsoft.AspNetCore.Http;

namespace Core.Interfaces.ExternalServices
{
    public interface IBlobStorageService
    {
        Task<IEnumerable<string>> UploadChatImagesAsync(IEnumerable<IFormFile> files, string container);
        Task<string> UploadProfileImageAsync(IFormFile file, string container, string userId);
        Task DeleteProfileImageAsync(string imageUrl, string container);
    }
}
