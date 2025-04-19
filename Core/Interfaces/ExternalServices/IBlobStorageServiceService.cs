using Microsoft.AspNetCore.Http;

namespace Core.Interfaces.ExternalServices
{
    public interface IBlobStorageServiceService
    {
        Task<string> UploadImageAsync(IFormFile file, string container);
    }
}
