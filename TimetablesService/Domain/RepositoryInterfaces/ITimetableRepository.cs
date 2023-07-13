using Domain.Entities;
using MongoDB.Bson;

namespace Domain.RepositoryInterfaces
{
    public interface ITimetableRepository
    {
        Task<List<RouteTimetable>> GetAllAsync();
        Task<RouteTimetable> GetByRouteIdAsync(Guid id);
        Task UpdateAsync(RouteTimetable timetable);
        void Add(RouteTimetable timetable);
        void Remove(RouteTimetable timetable);
    }
}
