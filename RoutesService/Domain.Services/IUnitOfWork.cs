using Domain.Entities;

namespace Domain.Services
{
    public interface IUnitOfWork
    {
        void AddRoute(Route route, ICollection<RouteStop> routeStops);
        void ReplaceRouteStops(Route route, ICollection<RouteStop> routeStops);
        void RemoveRoute(Route route);
        Task<ICollection<Route>> GetAllRoutesAsync();
        Task<ICollection<Route>> GetBusStopRoutesAsync(Guid busStopId);
        Task<ICollection<Route>> GetRoutesByRouteNameAsync(string routeName);
        Task<Route> GetRouteByIdAsync(Guid id);
        Task<ICollection<RouteStop>> GetAllRouteStopsAsync();
        Task<RouteStop> GetRouteStopByIdAsync(Guid id);
        Task<ICollection<BusStop>> GetAllBusStopsAsync();
        Task<BusStop> GetBusStopByIdAsync(Guid id);
        Task SaveChangesAsync();
    }
}
