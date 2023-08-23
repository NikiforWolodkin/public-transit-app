using Azure.Storage.Blobs.Models;

namespace Domain.ServiceRepositories
{
    public interface IBlobService
    {
        Task<ICollection<string>> ListBlobsAsync();
        Task<ICollection<string>> ListBlobsAsync(string prefix);
        Task UploadBlobAsync(Stream content, string blobName);
        Task DeleteBlobAsync(string blobName);
    }
}
