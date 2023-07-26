using Domain.Entities;
using Domain.Services.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BusStopRepository : IBusStopRepository
    {
        private readonly DataContext _context;

        public BusStopRepository(DataContext context)
        {
            _context = context;
        }

        void IBusStopRepository.Add(BusStop busStop)
        {
            _context.BusStops.Add(busStop);
        }

        void IBusStopRepository.Remove(BusStop busStop)
        {
            _context.BusStops.Remove(busStop);
        }

        async Task<IQueryable<BusStop>> IBusStopRepository.GetAllAsync()
        {
            return _context.BusStops.AsQueryable();
        }

        async Task<BusStop> IBusStopRepository.GetByIdAsync(Guid id)
        {
            return await _context.BusStops.FirstAsync(stop => stop.Id == id);
        }
    }
}
