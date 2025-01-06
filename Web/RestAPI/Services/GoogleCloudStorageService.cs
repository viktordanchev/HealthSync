using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using RestAPI.Services.Contracts;

namespace RestAPI.Services
{
    public class GoogleCloudStorageService : IGoogleCloudStorageService
    {
        private readonly string _bucketName = "healthsync";
        private readonly StorageClient _storageClient;

        public GoogleCloudStorageService()
        {
            var credentials = GoogleCredential.FromFile("Configs/gcp-credentials-service-account.json");
            _storageClient = StorageClient.Create(credentials);
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            var fileName = Path.GetFileName(file.FileName);
            var uniqueFileName = $"{Guid.NewGuid()}_{fileName}";

            using (var stream = file.OpenReadStream())
            {
                var uploadObject = await _storageClient.UploadObjectAsync(
                    _bucketName,
                    $"profile-images/{uniqueFileName}",
                    file.ContentType,
                    stream
                );

                return $"https://storage.cloud.google.com/healthsync/profile-images/{uniqueFileName}";
            }
        }
    }
}
