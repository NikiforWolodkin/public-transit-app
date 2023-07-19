using Domain.RepositoryInterfaces;
using MassTransit;
using Serilog;
using System.Reflection;
using TransitApplication.MessagingContracts;

namespace Web.Consumers
{
    public class RouteUpdatedConsumer : IConsumer<RouteUpdated>
    {
        private readonly ITimetableRepository _timetableRepository;

        public RouteUpdatedConsumer(ITimetableRepository timetableRepository)
        {
            _timetableRepository = timetableRepository;
        }

        public async Task Consume(ConsumeContext<RouteUpdated> context)
        {
            var methodName = this.GetType().Name;

            try
            {
                var route = await _timetableRepository.GetByRouteIdAsync(context.Message.Id);

                await _timetableRepository.UpdateAsync(route);

                Log.Information("{methodName}: Route updated => {@route}", methodName, route);
            }
            catch (InvalidOperationException ex)
            {
                Log.Error("{methodName}: Route not found. Exception =>  {ex}", methodName, ex);
            }
            catch (Exception ex)
            {
                Log.Error("{methodName}: Error! Exception =>  {ex}", methodName, ex);
            }
        }
    }
}
