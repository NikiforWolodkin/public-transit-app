using Domain.Entities;

namespace Domain.Services.RepositoryInterfaces
{
    public interface IRouteRepository
    {
        Task<ICollection<Route>> GetAllAsync();
        Task<ICollection<Route>> GetBusStopRoutes(Guid busStopId);
        Task<ICollection<Route>> GetByRouteNameAsync(string routeName);
        Task<Route> GetByIdAsync(Guid id);
        void Add(Route route);
        void Remove(Route route);
    }
}
