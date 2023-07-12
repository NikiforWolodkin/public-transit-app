using AutoMapper;
using Domain.Entities;
using Domain.Services;
using Domain.Services.RepositoryInterfaces;
using MassTransit;
using TransitApplication.MessagingContracts;
using Serilog;

namespace Web.Consumers
{
    public class BusStopAddedConsumer : IConsumer<BusStopAdded>
    {
        private readonly IBusStopRepository _busStopRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BusStopAddedConsumer(IBusStopRepository busStopRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _busStopRepository = busStopRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<BusStopAdded> context)
        {
            try
            {
                var busStop = _mapper.Map<BusStop>(context.Message);

                _busStopRepository.Add(busStop);

                await _unitOfWork.SaveChangesAsync();

                Log.Information("BusStopAddedConsumer: Bus stop added => {@busStop}", busStop);
            }
            catch (Exception ex)
            {
                Log.Error("BusStopAddedConsumer: Error! Exception =>  {@ex}", ex);
            }
        }
    }
}
