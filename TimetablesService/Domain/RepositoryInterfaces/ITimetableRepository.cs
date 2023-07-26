using Domain.Entities;
using MongoDB.Bson;

namespace Domain.RepositoryInterfaces
{
    public interface ITimetableRepository
    {
        /// <summary>
        /// Retrieves all the route timetables asynchronously.
        /// </summary>
        /// <returns>A list containing all the route timetables.</returns>
        Task<List<RouteTimetable>> GetAllAsync();

        /// <summary>
        /// Retrieves the timetable for a specific route asynchronously.
        /// </summary>
        /// <param name="id">The id of the route.</param>
        /// <returns>The object containing the timetable information of the route.</returns>
        /// <exception cref="InvalidOperationException">Thrown when a route with specified id is not found.</exception>
        Task<RouteTimetable> GetByRouteIdAsync(Guid id);

        /// <summary>
        /// Replaces a route timetable asynchronously.
        /// </summary>
        /// <param name="timetable">The updated timetable.</param>
        Task UpdateAsync(RouteTimetable timetable);

        /// <summary>
        /// Adds a new route timetable.
        /// </summary>
        /// <param name="timetable">The timetable to be added.</param>
        void Add(RouteTimetable timetable);

        /// <summary>
        /// Removes a given route timetable.
        /// </summary>
        /// <param name="timetable">The timetable to be removed.</param>
        void Remove(RouteTimetable timetable);

    }
}
