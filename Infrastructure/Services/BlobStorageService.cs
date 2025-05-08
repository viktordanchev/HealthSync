using Azure.Storage.Blobs;
using Core.Interfaces.ExternalServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services
{
    public class BlobStorageService : IBlobStorageService
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
                BlobClient blobClient = containerClient.GetBlobClient($"{Guid.NewGuid().ToString()}{Path.GetExtension(file.FileName)}");

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

            var fileName = $"{userId}{Path.GetExtension(file.FileName)}";
            BlobClient blobClient = containerClient.GetBlobClient(fileName);

            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, overwrite: true);
            }

            return blobClient.Uri.AbsoluteUri;
        }

        public async Task DeleteProfileImageAsync(string imageUrl, string container)
        {
            var uri = new Uri(imageUrl);
            
            BlobServiceClient blobServiceClient = new BlobServiceClient(_connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(container);
            BlobClient blobClient = containerClient.GetBlobClient(Path.GetFileName(uri.LocalPath));

            await blobClient.DeleteIfExistsAsync();
        }
    }
}
