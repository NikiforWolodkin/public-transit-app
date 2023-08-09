using Domain.ServiceRepositories;
using Microsoft.AspNetCore.Http;
using Services.Interfaces;

namespace Services.Services
{
    public class ImageService : IImageService
    {
        private readonly IBlobService _blobService;

        public ImageService(IBlobService blobService)
        {
            _blobService = blobService;
        }

        async Task IImageService.DeleteImageAsync(string imageName)
        {
            await _blobService.DeleteBlobAsync(imageName);
        }

        async Task<ICollection<string>> IImageService.GetAllImageNamesAsync()
        {
            var imageNames = await _blobService.ListBlobsAsync();

            return imageNames;
        }

        async Task<ICollection<string>> IImageService.GetImageNamesAsync(Guid busStopId)
        {
            var prefix = busStopId.ToString();
            var imageNames = await _blobService.ListBlobsAsync(prefix);

            return imageNames;
        }

        async Task<string> IImageService.UploadImageAsync(Guid busStopId, IFormFile image)
        {
            using var content = image.OpenReadStream();

            var fileExtension = Path.GetExtension(image.FileName);
            var date = DateTime.Now.ToString()
                .Replace(" ", "-")
                .Replace("/", "-")
                .Replace(":", "-");
            var blobName = $"{busStopId}-{date}{fileExtension}";

            await _blobService.UploadBlobAsync(content, blobName);

            return blobName;
        }
    }
}
