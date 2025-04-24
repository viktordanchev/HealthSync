using Azure.Storage.Blobs;
using Core.Interfaces.ExternalServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services
{
    public class BlobStorageService : IBlobStorageServiceService
    {
        private readonly string _connectionString;

        public BlobStorageService(IConfiguration configuration)
        {
            _connectionString = configuration["AzureStorage:ConnectionString"]!;
        }

        public async Task<IEnumerable<string>> UploadChatImagesAsync(IEnumerable<IFormFile> files, string container)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(_connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(container);

            List<string> imgUrls = new List<string>();

            foreach (var file in files)
            {
                BlobClient blobClient = containerClient.GetBlobClient(Guid.NewGuid().ToString());

                using (var stream = file.OpenReadStream())
                {
                    await blobClient.UploadAsync(stream, overwrite: true);
                }

                imgUrls.Add(blobClient.Uri.AbsoluteUri);
            }

            return imgUrls;
        }

        public async Task<string> UploadProfileImageAsync(IFormFile file, string container, string userId)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(_connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(container);
            BlobClient blobClient = containerClient.GetBlobClient(userId);

            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, overwrite: true);
            }

            return blobClient.Uri.AbsoluteUri;
        }

        public async Task<bool> DeleteProfileImageAsync(string fileName, string container)
        {
            try
            {
                BlobServiceClient blobServiceClient = new BlobServiceClient(_connectionString);
                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(container);
                BlobClient blobClient = containerClient.GetBlobClient(fileName);

                await blobClient.DeleteIfExistsAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
