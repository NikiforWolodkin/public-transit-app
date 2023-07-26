using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Dtos;
using Services.Interfaces;

namespace Presentation.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/routes")]
    public class RoutesController : ControllerBase
    {
        private readonly IRouteService _routeService;

        public RoutesController(IRouteService routeService)
        {
            _routeService = routeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var routes = await _routeService.GetAllAsync();

            return Ok(routes);
        }

        [HttpGet("{id:guid}")]
        [ActionName(nameof(GetByIdAsync))]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var route = await _routeService.GetByIdAsync(id);

            return Ok(route);
        }

        [HttpGet("search")]
        [ActionName(nameof(GetByIdAsync))]
        public async Task<IActionResult> SearchAsync(string? routeName, Guid? busStopId)
        {
            if (routeName is not null && busStopId is not null)
            {
                throw new NotImplementedException();
            }

            if (routeName is not null)
            {
                var routes = await _routeService.GetByRouteNameAsync(routeName);

                return Ok(routes);
            }

            if (busStopId is not null)
            {
                var routes = await _routeService.GetBusStopRoutesAsync((Guid)busStopId);

                return Ok(routes);
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(RouteAddDto routeAddDto)
        {
            var route = await _routeService.AddAsync(routeAddDto);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = route.Id }, route);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> ReplaceRouteStopsAsync(Guid id, List<RouteStopAddDto> routeStopsAddDto)
        {
            var route = await _routeService.ReplaceRouteStopsAsync(id, routeStopsAddDto);

            return Ok(route);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _routeService.RemoveAsync(id);

            return NoContent();
        }
    }
}
