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

        void IBusStopRepository.Delete(BusStop busStop)
        {
            _context.BusStops.Remove(busStop);
        }

        async Task<ICollection<BusStop>> IBusStopRepository.GetAllAsync()
        {
            return await _context.BusStops.ToListAsync();
        }

        async Task<BusStop> IBusStopRepository.GetByIdAsync(Guid id)
        {
            return await _context.BusStops.FirstAsync(stop => stop.Id == id);
        }
    }
}
