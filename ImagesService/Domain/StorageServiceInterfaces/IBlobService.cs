using Azure.Storage.Blobs.Models;

namespace Domain.ServiceRepositories
{
    public interface IBlobService
    {
        public Task<ICollection<string>> ListBlobsAsync();
        public Task<ICollection<string>> ListBlobsAsync(string prefix);
        public Task UploadBlobAsync(Stream content, string blobName);
        public Task DeleteBlobAsync(string blobName);
    }
}
