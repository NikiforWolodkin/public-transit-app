using Domain.Entities;

namespace Domain.Services.RepositoryInterfaces
{
    public interface IRouteStopRepository
    {
        Task<ICollection<RouteStop>> GetAllAsync();
        Task<RouteStop> GetByIdAsync(Guid id);
        void Add(RouteStop routeStop);
        void Remove(RouteStop routeStop);
    }
}
