using Domain.Entities;
using Domain.RepositoryInterfaces;
using Services.Dtos;
using Services.Interfaces;

namespace Services.Services
{
    public class TimetableService : ITimetableService
    {
        private readonly ITimetableRepository _timetableRepository;

        public TimetableService(ITimetableRepository timetableRepository)
        {
            _timetableRepository = timetableRepository;
        }

        async Task<BusStopTimetableDto> ITimetableService.GetBusStopTimetableAsync(Guid routeId, Guid busStopId)
        {
            var route = await _timetableRepository.GetByRouteIdAsync(routeId);

            var priorStops = route.RouteStops.TakeWhile(stop => stop.BusStopId != busStopId);

            var timeToAdd = priorStops
                .Select(stop => stop.IntervalToNextStop)
                .Aggregate((aggr, next) => aggr + next);

            var arrivalsTimetable = route.DepartureTimes
                .Select(time => time + timeToAdd)
                .ToList();

            var timetableDto = new BusStopTimetableDto
            {
                BusStopId = busStopId,
                RouteId = routeId,
                ArrivalsTimetable = arrivalsTimetable
            };

            return timetableDto;
        }

        async Task ITimetableService.UpdateAsync(Guid routeId, TimetableUpdateDto timetableUpdateDto)
        {
            var route = await _timetableRepository.GetByRouteIdAsync(routeId);

            route.DepartureTimes = timetableUpdateDto.DepartureTimes;

            await _timetableRepository.UpdateAsync(route);
        }
    }
}
