using Domain.Entities;

namespace Domain.Services.RepositoryInterfaces
{
    public interface IBusStopRepository
    {
        Task<IQueryable<BusStop>> GetAllAsync();
        Task<BusStop> GetByIdAsync(Guid id);
        void Add(BusStop busStop);
        void Remove(BusStop busStop);
    }
}
