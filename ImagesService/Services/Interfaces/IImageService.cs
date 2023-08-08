using Microsoft.AspNetCore.Http;

namespace Services.Interfaces
{
    public interface IImageService
    {
        Task<ICollection<string>> GetAllImageNamesAsync();
        Task<ICollection<string>> GetImageNamesAsync(Guid busStopId);
        Task<string> UploadImageAsync(Guid busStopId, IFormFile image);
        Task DeleteImageAsync(string imageName);
    }
}
