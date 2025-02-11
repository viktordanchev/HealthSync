using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Core.Interfaces.ExternalServices;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services
{
    public class GoogleCloudStorageService : IGoogleCloudStorageService
    {
        private readonly string _bucketName = "healthsync";
        private readonly StorageClient _storageClient;

        public GoogleCloudStorageService()
        {
            var credentials = GoogleCredential.FromFile("D:/Programming/HealthSync/Infrastructure/Services/Configs/gcp-credentials-service-account.json");
            _storageClient = StorageClient.Create(credentials);
        }

        public async Task<string> UploadProfileImageAsync(IFormFile file)
        {
            var fileName = Path.GetFileName(file.FileName);
            var uniqueFileName = $"{Guid.NewGuid()}-{fileName}";
            var destinationPath = $"profile-images/{uniqueFileName}";

            using (var stream = file.OpenReadStream())
            {
                await UploadFileAsync(
                    _bucketName,
                    destinationPath,
                    file.ContentType,
                    stream
                );
            }

            return $"https://storage.cloud.google.com/healthsync/profile-images/{uniqueFileName}";
        }

        private async Task UploadFileAsync(string bucketName, string destinationPath, string contentType, Stream fileStream)
        {
            await _storageClient.UploadObjectAsync(
                    bucketName,
                    destinationPath,
                    contentType,
                    fileStream
                );
        }
    }
}
