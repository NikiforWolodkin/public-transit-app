using AutoMapper;
using Domain.Services;
using Domain.Services.RepositoryInterfaces;
using MassTransit;
using TransitApplication.MessagingContracts;
using Serilog;

namespace Web.Consumers
{
    public class BusStopUpdatedConsumer : IConsumer<BusStopUpdated>
    {
        private readonly IBusStopRepository _busStopRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BusStopUpdatedConsumer(IBusStopRepository busStopRepository, IUnitOfWork unitOfWork)
        {
            _busStopRepository = busStopRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<BusStopUpdated> context)
        {
            var methodName = this.GetType().Name;

            try
            {
                var busStop = await _busStopRepository.GetByIdAsync(context.Message.Id);

                busStop.Type = context.Message.Type;

                await _unitOfWork.SaveChangesAsync();

                Log.Information("{methodName}: Bus stop updated => {@busStop}", busStop);
            }
            catch (InvalidOperationException ex)
            {
                Log.Error("{methodName}: Bus stop not found. Exception =>  {ex}", methodName, ex);
            }
            catch (Exception ex)
            {
                Log.Error("{methodName}: Error! Exception =>  {ex}", methodName, ex);
            }
        }
    }
}
