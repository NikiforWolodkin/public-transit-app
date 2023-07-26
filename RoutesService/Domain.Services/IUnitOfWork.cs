using Domain.Entities;

namespace Domain.Services
{
    public interface IUnitOfWork
    {
        void AddRoute(Route route, ICollection<RouteStop> routeStops);
        void ReplaceRouteStops(Route route, ICollection<RouteStop> routeStops);
        void RemoveRoute(Route route);
        Task<IQueryable<Route>> GetAllRoutesAsync();
        Task<IQueryable<Route>> GetBusStopRoutesAsync(Guid busStopId);
        Task<IQueryable<Route>> GetRoutesByRouteNameAsync(string routeName);
        Task<Route> GetRouteByIdAsync(Guid id);
        Task<IQueryable<RouteStop>> GetAllRouteStopsAsync();
        Task<RouteStop> GetRouteStopByIdAsync(Guid id);
        Task<IQueryable<BusStop>> GetAllBusStopsAsync();
        Task<BusStop> GetBusStopByIdAsync(Guid id);
        Task SaveChangesAsync();
    }
}
