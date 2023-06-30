using Domain.Entities;
using Domain.Services.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RouteStopRepository : IRouteStopRepository
    {
        private readonly DataContext _context;

        public RouteStopRepository(DataContext context)
        {
            _context = context;
        }

        void IRouteStopRepository.Add(RouteStop routeStop)
        {
            _context.RouteStops.Add(routeStop);
        }

        async Task<ICollection<RouteStop>> IRouteStopRepository.GetAllAsync()
        {
            return await _context.RouteStops.ToListAsync();
        }

        async Task<RouteStop> IRouteStopRepository.GetByIdAsync(Guid id)
        {
            return await _context.RouteStops.FirstAsync(stop => stop.Id == id);
        }

        void IRouteStopRepository.Remove(RouteStop routeStop)
        {
            _context.RouteStops.Remove(routeStop);
        }
    }
}
