using AutoMapper;
using Domain.Entities;
using Domain.Services;
using Domain.Services.RepositoryInterfaces;
using MassTransit;
using TransitApplication.MessagingContracts;
using Serilog;

namespace Web.Consumers
{
    public class BusStopRemovedConsumer : IConsumer<BusStopRemoved>
    {
        private readonly IBusStopRepository _busStopRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BusStopRemovedConsumer(IBusStopRepository busStopRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _busStopRepository = busStopRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<BusStopRemoved> context)
        {
            var methodName = this.GetType().Name;

            try
            {
                var busStop = _mapper.Map<BusStop>(context.Message);

                _busStopRepository.Remove(busStop);

                await _unitOfWork.SaveChangesAsync();

                Log.Information("{methodName}: Bus stop removed => {@busStop}", methodName, busStop);
            }
            catch (InvalidOperationException ex)
            {
                Log.Error("{methodName}: Bus stop not found. Exception =>  {ex}", ex);
            }
            catch (Exception ex)
            {
                Log.Error("{methodName}: Error! Exception =>  {ex}", methodName, ex);
            }
        }
    }
}
