namespace RestAPI.Services.Contracts
{
    public interface IGoogleCloudStorageService
    {
        Task<string> UploadProfileImageAsync(IFormFile file);
    }
}
