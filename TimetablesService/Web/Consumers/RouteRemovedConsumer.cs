using Domain.RepositoryInterfaces;
using MassTransit;
using Serilog;
using System.Reflection;
using TransitApplication.MessagingContracts;

namespace Web.Consumers
{
    public class RouteRemovedConsumer : IConsumer<RouteRemoved>
    {
        private readonly ITimetableRepository _timetableRepository;

        public RouteRemovedConsumer(ITimetableRepository timetableRepository)
        {
            _timetableRepository = timetableRepository;
        }

        public async Task Consume(ConsumeContext<RouteRemoved> context)
        {
            var methodName = this.GetType().Name;

            try
            {
                var route = await _timetableRepository.GetByRouteIdAsync(context.Message.Id);

                _timetableRepository.Remove(route);

                Log.Information("{methodName}: Route removed => {@route}", methodName, route);
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
