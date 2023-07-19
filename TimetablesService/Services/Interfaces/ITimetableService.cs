using Services.Dtos;
using TransitApplication.HttpExceptions;

namespace Services.Interfaces
{
    public interface ITimetableService
    {
        /// <summary>
        /// Updates the timetable of a route asynchronously.
        /// </summary>
        /// <param name="routeId">The id of the route.</param>
        /// <param name="timetableUpdateDto">The object containing the updated timetable information.</param>
        /// <exception cref="NotFoundException">Thrown when a route with specified id is not found.</exception>
        /// <exception cref="InternalServerErrorException">Thrown when an internal server error occurs.</exception>
        Task UpdateAsync(Guid routeId, TimetableUpdateDto timetableUpdateDto);

        /// <summary>
        /// Retrieves the timetable for a specific bus stop on a route asynchronously.
        /// </summary>
        /// <param name="routeId">The id of the route.</param>
        /// <param name="busStopId">The id of the bus stop.</param>
        /// <returns>The object containing the timetable information for the specified bus stop.</returns>
        /// <exception cref="NotFoundException">Thrown when a route or a bus stop with specified id is not found.</exception>
        /// <exception cref="InternalServerErrorException">Thrown when an internal server error occurs.</exception>
        Task<BusStopTimetableDto> GetBusStopTimetableAsync(Guid routeId, Guid busStopId);
    }
}
