using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Core.Interfaces.ExternalServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Services
{
    public class GoogleCloudStorageService : IGoogleCloudStorageService
    {
        private readonly string _bucketName = "healthsync";
        private readonly StorageClient _storageClient;

        public GoogleCloudStorageService(IHostEnvironment environment)
        {
            GoogleCredential credentials;

            if (environment.IsDevelopment())
            {
                credentials = GoogleCredential.FromFile("Infrastructure/Services/Configs/gcp-credentials-service-account.json");
            }
            else
            {
                var jsonCredentials = Environment.GetEnvironmentVariable("GCP_CREDENTIALS");
                credentials = GoogleCredential.FromJson(jsonCredentials);
            }
                
            _storageClient = StorageClient.Create(credentials);
        }
        
        public async Task<string> UploadImageAsync(IFormFile file, string directory)
        {
            var fileName = Path.GetFileName(file.FileName);
            var uniqueFileName = $"{Guid.NewGuid()}-{fileName}";
            var destinationPath = $"{directory}/{uniqueFileName}";

            using (var stream = file.OpenReadStream())
            {
                await UploadFileAsync(
                    _bucketName,
                    destinationPath,
                    file.ContentType,
                    stream
                );
            }

            return $"https://storage.cloud.google.com/healthsync/{destinationPath}";
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
