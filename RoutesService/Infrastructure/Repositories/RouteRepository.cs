using Domain.Entities;
using Domain.Services.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RouteRepository : IRouteRepository
    {
        private readonly DataContext _context;

        public RouteRepository(DataContext context)
        {
            _context = context;
        }

        void IRouteRepository.Add(Route route)
        {
            _context.Routes.Add(route);
        }

        async Task<ICollection<Route>> IRouteRepository.GetAllAsync()
        {
            return await _context.Routes
                .Include(route => route.RouteStops)
                .ThenInclude(stop => stop.BusStop)
                .ToListAsync();
        }

        async Task<ICollection<Route>> IRouteRepository.GetBusStopRoutes(Guid busStopId)
        {
            return await _context.Routes
                .Include(route => route.RouteStops)
                .ThenInclude(stop => stop.BusStop)
                .Where(route => route.RouteStops
                    .Select(stop => stop.BusStop.Id)
                    .Contains(busStopId))
                .ToListAsync();
        }

        async Task<Route> IRouteRepository.GetByIdAsync(Guid id)
        {
            return await _context.Routes
                .Include(route => route.RouteStops)
                .ThenInclude(stop => stop.BusStop)
                .FirstAsync(route => route.Id == id);
        }

        async Task<ICollection<Route>> IRouteRepository.GetByRouteNameAsync(string routeName)
        {
            return await _context.Routes
                .Include(route => route.RouteStops)
                .ThenInclude(stop => stop.BusStop)
                .Where(route => route.Name == routeName)
                .ToListAsync();
        }

        void IRouteRepository.Remove(Route route)
        {
            _context.Routes.Remove(route);
        }
    }
}
