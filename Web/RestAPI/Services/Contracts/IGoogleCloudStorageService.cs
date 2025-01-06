namespace RestAPI.Services.Contracts
{
    public interface IGoogleCloudStorageService
    {
        Task<string> UploadFileAsync(IFormFile file);
    }
}
