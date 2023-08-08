using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Domain.ServiceRepositories;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Infrastructure.StorageServices
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly IConfiguration _configuration;
        private static readonly FileExtensionContentTypeProvider s_fileExtensionProvider = new FileExtensionContentTypeProvider();

        public BlobService(BlobServiceClient blobServiceClient, IConfiguration configuration)
        {
            _blobServiceClient = blobServiceClient;
            _configuration = configuration;
        }

        async Task IBlobService.DeleteBlobAsync(string blobName)
        {
            var blobContainerName = _configuration.GetValue<string>("AzureBlobStorage:ContainerName");
            var containerClient = _blobServiceClient.GetBlobContainerClient(blobContainerName);
            var blobClient = containerClient.GetBlobClient(blobName);

            await blobClient.DeleteIfExistsAsync();
        }

        async Task<ICollection<string>> IBlobService.ListBlobsAsync()
        {
            var blobContainerName = _configuration.GetValue<string>("AzureBlobStorage:ContainerName");
            var containerClient = _blobServiceClient.GetBlobContainerClient(blobContainerName);

            var blobs = new List<string>();
            await foreach (var blob in containerClient.GetBlobsAsync())
            {
                blobs.Add(blob.Name);
            }

            return blobs;
        }

        async Task IBlobService.UploadBlobAsync(Stream content, string blobName)
        {
            var blobContainerName = _configuration.GetValue<string>("AzureBlobStorage:ContainerName");
            var containerClient = _blobServiceClient.GetBlobContainerClient(blobContainerName);
            var blobClient = containerClient.GetBlobClient(blobName);

            await blobClient.UploadAsync(content, new BlobHttpHeaders
            {
                ContentType = GetContentType(blobName)
            });
        }

        private static string GetContentType(string fileName)
        {
            if (!s_fileExtensionProvider.TryGetContentType(fileName, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            return contentType;
        }

        async Task<ICollection<string>> IBlobService.ListBlobsAsync(string prefix)
        {
            var blobContainerName = _configuration.GetValue<string>("AzureBlobStorage:ContainerName");
            var containerClient = _blobServiceClient.GetBlobContainerClient(blobContainerName);

            var blobs = new List<string>();
            await foreach (var blob in containerClient.GetBlobsAsync(prefix: prefix))
            {
                blobs.Add(blob.Name);
            }

            return blobs;
        }
    }
}
