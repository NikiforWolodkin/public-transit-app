using Domain.Entities;
using Domain.Services.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using TransitApplication.Enums;

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

        async Task<IQueryable<Route>> IRouteRepository.GetAllAsync()
        {
            return _context.Routes.AsQueryable();
        }

        async Task<IQueryable<Route>> IRouteRepository.GetBusStopRoutes(Guid busStopId)
        {
            return _context.Routes
                .Where(route => route.RouteStops
                    .Select(stop => stop.BusStop.Id)
                    .Contains(busStopId))
                .AsQueryable();
        }

        async Task<Route> IRouteRepository.GetByIdAsync(Guid id)
        {
            return await _context.Routes.FirstAsync(route => route.Id == id);
        }

        async Task<IQueryable<Route>> IRouteRepository.SearchByRouteNameAsync(string routeName)
        {
            return _context.Routes
                .Where(route => route.Name.ToUpper().Contains(routeName.ToUpper()))
                .AsQueryable();
        }

        void IRouteRepository.Remove(Route route)
        {
            _context.Routes.Remove(route);
        }
    }
}
