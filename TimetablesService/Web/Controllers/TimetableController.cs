using Microsoft.AspNetCore.Mvc;
using Services.Dtos;
using Services.Interfaces;

namespace Web.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/timetables")]
    public class TimetableController : ControllerBase
    {
        private readonly ITimetableService _timetableService;

        public TimetableController(ITimetableService timetableService)
        {
            _timetableService = timetableService;
        }

        [HttpGet("{routeId:guid}&{busStopId:guid}")]
        public async Task<IActionResult> GetBusGetBusStopTimetableAsync(Guid routeId, Guid busStopId)
        {
            var timetable = await _timetableService.GetBusStopTimetableAsync(routeId, busStopId);

            return Ok(timetable);
        }

        [HttpPut("{routeId:guid}")]
        public async Task<IActionResult> UpdateAsync(Guid routeId, TimetableUpdateDto timetableUpdateDto)
        {
            await _timetableService.UpdateAsync(routeId, timetableUpdateDto);

            return NoContent();
        }
    }
}
