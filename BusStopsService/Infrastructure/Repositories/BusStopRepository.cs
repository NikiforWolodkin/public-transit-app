using Domain.Entities;
using Domain.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Infrastructure.Repositories
{
    public class BusStopRepository : IBusStopRepository
    {
        private const double NearbyDistance = 100000; // 10km
        private readonly DataContext _dataContext;

        public BusStopRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        void IBusStopRepository.Add(BusStop busStop)
        {
            _dataContext.BusStops.Add(busStop);
        }

        async Task<ICollection<BusStop>> IBusStopRepository.GetAllAsync()
        {
            return await _dataContext.BusStops.ToListAsync();
        }

        async Task<BusStop> IBusStopRepository.GetByIdAsync(Guid id)
        {
            return await _dataContext.BusStops.FirstAsync(busStop => busStop.Id == id);
        }

        async Task<ICollection<BusStop>> IBusStopRepository.GetNearbyAsync(Point location)
        {
            return await _dataContext.BusStops
                .Where(busStop => busStop.Coordinates.Distance(location) < NearbyDistance)
                .ToListAsync();
        }

        void IBusStopRepository.Remove(BusStop busStop)
        {
            _dataContext.BusStops.Remove(busStop);
        }

        async Task IBusStopRepository.SaveChangesAsync()
        {
            await _dataContext.SaveChangesAsync();
        }

        async Task<ICollection<BusStop>> IBusStopRepository.SearchByBusStopNameAsync(string busStopName)
        {
            return await _dataContext.BusStops
                .Where(busStop => busStop.Name.ToUpper().Contains(busStopName.ToUpper()))
                .ToListAsync();
        }
    }
}
