using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Services.Dtos;
using Services.Interfaces;
using TransitApplication.HttpExceptions;

namespace Web.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/timetables")]
    public class TimetablesController : ControllerBase
    {
        private readonly ITimetableService _timetableService;
        private readonly IValidator<DepartureTimetableAddDto> _timetableUpdateValidator;

        public TimetablesController(ITimetableService timetableService, IValidator<DepartureTimetableAddDto> timetableUpdateValidator)
        {
            _timetableService = timetableService;
            _timetableUpdateValidator = timetableUpdateValidator;
        }

        /// <summary>
        /// Retrieves the timetable for a specific bus stop on a route.
        /// </summary>
        [HttpGet("{routeId:guid}&{busStopId:guid}")]
        [ProducesResponseType(200, Type = typeof(ICollection<BusStopTimetableDto>))]
        [ProducesResponseType(500)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetBusGetBusStopTimetableAsync(Guid routeId, Guid busStopId)
        {
            var timetable = await _timetableService.GetBusStopTimetableAsync(routeId, busStopId);

            return Ok(timetable);
        }

        /// <summary>
        /// Updates the departure times of the transport on a route.
        /// </summary>
        [HttpPut("{routeId:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateAsync(Guid routeId, List<DepartureTimetableAddDto> departureTimetablesAddDto)
        {
            await _timetableService.UpdateAsync(routeId, departureTimetablesAddDto);

            foreach (var departureTimetable in departureTimetablesAddDto)
            {
                var validationResults = await _timetableUpdateValidator.ValidateAsync(departureTimetable);

                if (!validationResults.IsValid)
                {
                    var error = validationResults.Errors.First();

                    var errorMessage = $"{error.ErrorCode}: {error.ErrorMessage}";

                    throw new BadRequestException("Departure times are invalid.", errorMessage);
                }
            }

            return NoContent();
        }
    }
}
