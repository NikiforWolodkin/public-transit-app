using AutoMapper;
using Domain.Entities;
using TransitApplication.MessagingContracts;

namespace Web.Profiles
{
    public class RouteMessagingProfile : Profile
    {
        public RouteMessagingProfile()
        {
            CreateMap<RouteAdded, RouteTimetable>()
                .ForMember(destination => destination.Id, options => options.Ignore())
                .ForMember(destination => destination.RouteId, options => options.MapFrom(
                    source => source.Id))
                .ForMember(destination => destination.RouteStops, options => options.MapFrom(
                    source => source.RouteStops.Select(stop => new RouteStop
                    {
                        BusStopId = stop.BusStopId,
                        IntervalToNextStop = stop.IntervalToNextStop
                    })
                    .ToList()));

            CreateMap<RouteRemoved, RouteTimetable>()
                .ForMember(destination => destination.Id, options => options.Ignore())
                .ForMember(destination => destination.RouteId, options => options.MapFrom(
                    source => source.Id))
                .ForMember(destination => destination.RouteStops, options => options.MapFrom(
                    source => source.RouteStops.Select(stop => new RouteStop
                    {
                        BusStopId = stop.BusStopId,
                        IntervalToNextStop = stop.IntervalToNextStop
                    })
                    .ToList()));

            CreateMap<RouteUpdated, RouteTimetable>()
                .ForMember(destination => destination.Id, options => options.Ignore())
                .ForMember(destination => destination.RouteId, options => options.MapFrom(
                    source => source.Id))
                .ForMember(destination => destination.RouteStops, options => options.MapFrom(
                    source => source.RouteStops.Select(stop => new RouteStop
                    {
                        BusStopId = stop.BusStopId,
                        IntervalToNextStop = stop.IntervalToNextStop
                    })
                    .ToList()));
        }
    }
}
