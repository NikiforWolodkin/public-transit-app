using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions.Dtos;
using Services.Interfaces;
using TransitApplication.HttpExceptions;

namespace Presentation.Controllers
{
    [Authorize]
    [ApiController]
    [Route("bus-stops-api/bus-stops")]
    public class BusStopsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IValidator<BusStopAddDto> _busStopAddValidator;
        private readonly IValidator<BusStopUpdateDto> _busStopUpdateValidator;

        public BusStopsController(IServiceManager serviceManager, IValidator<BusStopAddDto> busStopAddValidator,
            IValidator<BusStopUpdateDto> busStopUpdateValidator)
        {
            _serviceManager = serviceManager;
            _busStopAddValidator = busStopAddValidator;
            _busStopUpdateValidator = busStopUpdateValidator;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<BusStopDto>))]
        [ProducesResponseType(500)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetAllAsync()
        {
            var busStops = await _serviceManager.BusStopService.GetAllAsync();

            return Ok(busStops);   
        }

        [HttpGet("{id:guid}")]
        [ActionName(nameof(GetByIdAsync))]
        [ProducesResponseType(200, Type = typeof(BusStopDto))]
        [ProducesResponseType(500)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var busStop = await _serviceManager.BusStopService.GetByIdAsync(id);

            return Ok(busStop);
        }

        [ActionName(nameof(GetByIdAsync))]
        [HttpGet("{longitude:double}&{latitude:double}")]
        [ProducesResponseType(200, Type = typeof(ICollection<BusStopDto>))]
        [ProducesResponseType(500)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetNearbyAsync(double longitude, double latitude)
        {
            var busStops = await _serviceManager.BusStopService.GetNearbyAsync(longitude, latitude);

            return Ok(busStops);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(BusStopDto))]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> AddAsync(BusStopAddDto busStopAddDto)
        {
            var validationResults = await _busStopAddValidator.ValidateAsync(busStopAddDto);

            if (!validationResults.IsValid)
            {
                var error = validationResults.Errors.First();

                var errorMessage = $"{error.ErrorCode}: {error.ErrorCode}";

                throw new BadRequestException(errorMessage);
            }

            var busStop = await _serviceManager.BusStopService.AddAsync(busStopAddDto);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = busStop.Id }, busStop);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(200, Type = typeof(BusStopDto))]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateAsync(Guid id, BusStopUpdateDto busStopUpdateDto)
        {
            var validationResults = await _busStopUpdateValidator.ValidateAsync(busStopUpdateDto);

            if (!validationResults.IsValid)
            {
                var error = validationResults.Errors.First();

                var errorMessage = $"{error.ErrorCode}: {error.ErrorCode}";

                throw new BadRequestException(errorMessage);
            }

            var busStop = await _serviceManager.BusStopService.UpdateAsync(id, busStopUpdateDto);

            return Ok(busStop);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
            await _serviceManager.BusStopService.RemoveAsync(id);

            return NoContent();
        }
    }
}
