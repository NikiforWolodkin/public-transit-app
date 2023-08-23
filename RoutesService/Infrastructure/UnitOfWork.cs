using Domain.Entities;
using Domain.Services;
using Domain.Services.RepositoryInterfaces;
using Infrastructure.Data;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private readonly IBusStopRepository _busStopRepository;
        private readonly IRouteStopRepository _routeStopRepository;
        private readonly IRouteRepository _routeRepository;

        public UnitOfWork(DataContext context, IBusStopRepository busStopRepository,
            IRouteStopRepository routeStopRepository, IRouteRepository routeRepository)
        {
            _context = context;
            _busStopRepository = busStopRepository;
            _routeStopRepository = routeStopRepository;
            _routeRepository = routeRepository;
        }

        void IUnitOfWork.AddRoute(Route route, ICollection<RouteStop> routeStops)
        {
            foreach (var routeStop in routeStops)
            {
                _routeStopRepository.Add(routeStop);
            }

            route.RouteStops = routeStops;

            _routeRepository.Add(route);
        }

        async Task<IQueryable<BusStop>> IUnitOfWork.GetAllBusStopsAsync()
        {
            return await _busStopRepository.GetAllAsync();
        }

        async Task<IQueryable<Route>> IUnitOfWork.GetAllRoutesAsync()
        {
            return await _routeRepository.GetAllAsync();
        }

        async Task<IQueryable<RouteStop>> IUnitOfWork.GetAllRouteStopsAsync()
        {
            return await _routeStopRepository.GetAllAsync();
        }

        async Task<BusStop> IUnitOfWork.GetBusStopByIdAsync(Guid id)
        {
            return await _busStopRepository.GetByIdAsync(id);
        }

        async Task<IQueryable<Route>> IUnitOfWork.GetBusStopRoutesAsync(Guid busStopId)
        {
            return await _routeRepository.GetBusStopRoutes(busStopId);
        }

        async Task<Route> IUnitOfWork.GetRouteByIdAsync(Guid id)
        {
            return await _routeRepository.GetByIdAsync(id);
        }

        async Task<IQueryable<Route>> IUnitOfWork.SearchRoutesByNameAsync(string routeName)
        {
            return await _routeRepository.SearchByRouteNameAsync(routeName);
        }

        async Task<RouteStop> IUnitOfWork.GetRouteStopByIdAsync(Guid id)
        {
            return await _routeStopRepository.GetByIdAsync(id);
        }

        void IUnitOfWork.RemoveRoute(Route route)
        {
            foreach (var routeStop in route.RouteStops)
            {
                _routeStopRepository.Remove(routeStop);
            }

            _routeRepository.Remove(route);
        }

        void IUnitOfWork.ReplaceRouteStops(Route route, ICollection<RouteStop> routeStops)
        {
            foreach (var routeStop in route.RouteStops)
            {
                _routeStopRepository.Remove(routeStop);
            }

            foreach (var routeStop in routeStops)
            {
                _routeStopRepository.Add(routeStop);
            }

            route.RouteStops = routeStops;
        }

        async Task IUnitOfWork.SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
