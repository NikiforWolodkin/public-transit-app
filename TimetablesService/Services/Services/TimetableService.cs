using AutoMapper;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Serilog;
using Services.Dtos;
using Services.Interfaces;
using System.Runtime.CompilerServices;
using TransitApplication.HttpExceptions;

namespace Services.Services
{
    public class TimetableService : ITimetableService
    {
        private readonly ITimetableRepository _timetableRepository;
        private readonly IMapper _mapper;

        public TimetableService(ITimetableRepository timetableRepository, IMapper mapper)
        {
            _timetableRepository = timetableRepository;
            _mapper = mapper;
        }

        async Task<BusStopTimetableDto> ITimetableService.GetBusStopTimetableAsync(Guid routeId, Guid busStopId)
        {
            var methodName = GetCurrentMethodName();

            try
            {
                var route = await _timetableRepository.GetByRouteIdAsync(routeId);

                if (!route.RouteStops.Any(stop => stop.BusStopId == busStopId))
                {
                    throw new NotFoundException("Bus stop not found.");
                }

                var priorStops = route.RouteStops.TakeWhile(stop => stop.BusStopId != busStopId);

                var timeToAdd = TimeSpan.Zero;
                if (priorStops.Any())
                {
                    // Calculate the total time to add based on the intervals between prior stops.
                    timeToAdd = (TimeSpan)priorStops
                        .Select(stop => stop.IntervalToNextStop)
                        .Aggregate((aggr, next) => aggr + next);
                }

                // Add the calculated time to the departure times
                // to get the arrival times at the specified bus stop.
                var arrivalsTimetables = route.DepartureTimetables
                    .Select(table =>
                    {
                        table.DepartureTimes = table.DepartureTimes
                            .Select(time => time + timeToAdd)
                            .ToList();

                        return table;
                    })
                    .ToList();

                var timetableDto = new BusStopTimetableDto
                {
                    BusStopId = busStopId,
                    RouteId = routeId,
                    ArrivalsTimetables = _mapper.Map<List<ArrivalsTimetableDto>>(arrivalsTimetables)
                };

                return timetableDto;
            }
            catch (NotFoundException ex)
            {
                Log.Information("{methodName}: Route not found. Exception =>  {ex}", methodName, ex);

                throw ex;
            }
            catch (InvalidOperationException ex)
            {
                Log.Information("{methodName}: Route not found. Exception =>  {ex}", methodName, ex);

                throw new NotFoundException("Route not found.", $"{ex.GetType().Name}: {ex.Message}");
            }
            catch (Exception ex)
            {
                Log.Error("{methodName}: Error! Exception =>  {ex}", methodName, ex);

                throw new InternalServerErrorException($"{ex.GetType().Name}: {ex.Message}");
            }
        }

        async Task ITimetableService.UpdateAsync(Guid routeId, List<DepartureTimetableAddDto> departureTimetablesAddDto)
        {
            var methodName = GetCurrentMethodName();

            try
            {
                var route = await _timetableRepository.GetByRouteIdAsync(routeId);

                route.DepartureTimetables = _mapper.Map<List<DepartureTimetable>>(departureTimetablesAddDto);

                await _timetableRepository.UpdateAsync(route);
            }
            catch (InvalidOperationException ex)
            {
                Log.Information("{methodName}: Route not found. Exception =>  {ex}", methodName, ex);

                throw new NotFoundException("Route not found.", $"{ex.GetType().Name}: {ex.Message}");
            }
            catch (Exception ex)
            {
                Log.Error("{methodName}: Error! Exception =>  {ex}", methodName, ex);

                throw new InternalServerErrorException($"{ex.GetType().Name}: {ex.Message}");
            }
        }

        private string GetCurrentMethodName([CallerMemberName] string callerName = "")
        {
            return callerName;
        }
    }
}
