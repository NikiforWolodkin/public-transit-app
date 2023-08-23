using Domain.Entities;

namespace Domain.Services.RepositoryInterfaces
{
    public interface IRouteRepository
    {
        Task<IQueryable<Route>> GetAllAsync();
        Task<IQueryable<Route>> GetBusStopRoutes(Guid busStopId);
        Task<IQueryable<Route>> SearchByRouteNameAsync(string routeName);
        Task<Route> GetByIdAsync(Guid id);
        void Add(Route route);
        void Remove(Route route);
    }
}
