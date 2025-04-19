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

        public async Task<string> UploadImageAsync(IFormFile file, string container)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(_connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(container);

            var fileName = $"{Guid.NewGuid()}-{Path.GetFileName(file.FileName)}";
            BlobClient blobClient = containerClient.GetBlobClient(fileName);

            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, overwrite: true);
            }

            return blobClient.Uri.AbsoluteUri;
        }
    }
}
