using Domain.Entities;
using NetTopologySuite.Geometries;

namespace Domain.RepositoryInterfaces
{
    public interface IBusStopRepository
    {
        Task<ICollection<BusStop>> GetAllAsync();
        Task<ICollection<BusStop>> GetNearbyAsync(Point location);
        Task<ICollection<BusStop>> SearchByBusStopNameAsync(string busStopName);
        Task<BusStop> GetByIdAsync(Guid id);
        void Add(BusStop busStop);
        void Remove(BusStop busStop);
        Task SaveChangesAsync();
    }
}
