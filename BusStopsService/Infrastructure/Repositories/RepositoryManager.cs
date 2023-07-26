using Domain.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly Lazy<IBusStopRepository> _lazyBusStopRepository;

        public RepositoryManager(DataContext context)
        {
            _lazyBusStopRepository = new Lazy<IBusStopRepository>(() => new BusStopRepository(context));
        }

        IBusStopRepository IRepositoryManager.BusStopRepository => _lazyBusStopRepository.Value;
    }
}
