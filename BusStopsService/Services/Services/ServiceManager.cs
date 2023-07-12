using AutoMapper;
using Domain.RepositoryInterfaces;
using MassTransit;
using Services.Abstractions.Interfaces;
using Services.Interfaces;

namespace Services.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IBusStopService> _lazyBusStopService;

        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _lazyBusStopService = new Lazy<IBusStopService>(
                () => new BusStopService(repositoryManager, mapper, publishEndpoint));
        }

        IBusStopService IServiceManager.BusStopService => _lazyBusStopService.Value;
    }
}
