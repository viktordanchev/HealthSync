using Microsoft.AspNetCore.Http;

namespace Core.Interfaces.ExternalServices
{
    public interface IBlobStorageServiceService
    {
        Task<IEnumerable<string>> UploadChatImagesAsync(IEnumerable<IFormFile> files, string container);
        Task<string> UploadProfileImageAsync(IFormFile file, string container, string userId);
        Task<bool> DeleteProfileImageAsync(string fileName, string container);
    }
}
