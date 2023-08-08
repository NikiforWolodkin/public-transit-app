using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Web.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/images")]
    public class ImagesController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImagesController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllImageNames()
        {
            var imageNames = await _imageService.GetAllImageNamesAsync();

            return Ok(imageNames);
        }

        [HttpPost("{id:guid}")]
        public async Task<IActionResult> UploadImage(Guid id, IFormFile image)
        {
            var imageName = await _imageService.UploadImageAsync(id, image);

            return Created($"api/images/{id}", imageName);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetImageNames(Guid id)
        {
            var imageNames = await _imageService.GetImageNamesAsync(id);

            return Ok(imageNames);
        }

        [HttpDelete("{imageName}")]
        public async Task<IActionResult> DeleteImage(string imageName)
        {
            await _imageService.DeleteImageAsync(imageName);

            return NoContent();
        }
    }
}
