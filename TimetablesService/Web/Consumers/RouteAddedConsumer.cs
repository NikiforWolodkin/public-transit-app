using AutoMapper;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using MassTransit;
using Serilog;
using Services.Interfaces;
using System.Reflection;
using TransitApplication.MessagingContracts;

namespace Web.Consumers
{
    public class RouteAddedConsumer : IConsumer<RouteAdded>
    {
        private readonly IMapper _mapper;
        private readonly ITimetableRepository _timetableRepository;

        public RouteAddedConsumer(IMapper mapper, ITimetableRepository timetableRepository)
        {
            _mapper = mapper;
            _timetableRepository = timetableRepository;
        }

        public async Task Consume(ConsumeContext<RouteAdded> context)
        {
            var methodName = this.GetType().Name;

            try
            {
                var route = _mapper.Map<RouteTimetable>(context.Message);

                _timetableRepository.Add(route);

                Log.Information("{methodName}: Route added => {@route}", methodName, route);

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Log.Error("{methodName}: Error! Exception =>  {ex}", methodName, ex);
            }
        }
    }
}
