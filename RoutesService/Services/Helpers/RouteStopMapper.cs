using Domain.Entities;
using Domain.Services;
using Services.Dtos;

namespace Services.Helpers
{
    internal static class RouteStopMapper
    {
        /// <summary>
        /// Maps a single RouteStopAddDto object to a RouteStop object asynchronously.
        /// </summary>
        /// <param name="routeStopAddDto">The RouteStopAddDto object to map.</param>
        /// <param name="unitOfWork">The IUnitOfWork object used to retrieve BusStop objects.</param>
        /// <returns>RouteStop object.</returns>
        /// <exception cref="InvalidOperationException">Thrown when a bus stop with specified id is not found.</exception>
        public static async Task<RouteStop> MapFromDtoAsync
            (RouteStopAddDto routeStopAddDto, IUnitOfWork unitOfWork)
        {
            var busStop = await unitOfWork.GetBusStopByIdAsync(routeStopAddDto.BusStopId);

            var nextBusStop = routeStopAddDto.NextBusStopId == null ? null 
                : await unitOfWork.GetBusStopByIdAsync((Guid)routeStopAddDto.NextBusStopId);

            var routeStop = new RouteStop
            {
                BusStop = busStop,
                NextBusStop = nextBusStop,
                IntervalToNextStop = routeStopAddDto.IntervalToNextStop
            };

            return routeStop;
        }

        /// <summary>
        /// Maps a list of RouteStopAddDto objects to a list of RouteStop objects asynchronously.
        /// </summary>
        /// <param name="routeStopsAddDto">The list of RouteStopAddDto objects to map.</param>
        /// <param name="unitOfWork">The IUnitOfWork object used to retrieve BusStop objects.</param>
        /// <returns>List of RouteStop objects.</returns>
        /// <exception cref="InvalidOperationException">Thrown when a bus stop with specified id is not found.</exception>
        public static async Task<List<RouteStop>> MapFromDtoAsync
            (List<RouteStopAddDto> routeStopsAddDto, IUnitOfWork unitOfWork)
        {
            var routeStops = new List<RouteStop>();

            foreach (var routeStopAddDto in routeStopsAddDto)
            {
                var busStop = await unitOfWork.GetBusStopByIdAsync(routeStopAddDto.BusStopId);

                var nextBusStop = routeStopAddDto.NextBusStopId == null ? null
                    : await unitOfWork.GetBusStopByIdAsync((Guid)routeStopAddDto.NextBusStopId);

                var routeStop = new RouteStop
                {
                    BusStop = busStop,
                    NextBusStop = nextBusStop,
                    IntervalToNextStop = routeStopAddDto.IntervalToNextStop
                };

                routeStops.Add(routeStop);
            }

            return routeStops;
        }
    }
}
