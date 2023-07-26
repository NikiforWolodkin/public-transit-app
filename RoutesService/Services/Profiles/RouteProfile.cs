using AutoMapper;
using Domain.Entities;
using Services.Dtos;
using TransitApplication.MessagingContracts;

namespace Services.Profiles
{
    public class RouteProfile : Profile
    {
        public RouteProfile()
        {
            CreateMap<RouteStopAddDto, RouteStop>();

            CreateMap<RouteStop, RouteStopDto>()
                .ForMember(destination => destination.BusStopId, options => options.MapFrom(
                    source => source.BusStop.Id
                ));

            CreateMap<RouteAddDto, Route>();

            CreateMap<Route, RouteDto>()
                .ForMember(destination => destination.RouteStops, options => options.MapFrom(
                    source => source.RouteStops
                        .Select(stop => new RouteStopDto
                        {
                            Id = stop.Id,
                            BusStopId = stop.BusStop.Id,
                            IntervalToNextStop = stop.IntervalToNextStop,
                        })
                        .ToList()
                ));



            CreateMap<Route, RouteAdded>()
                .ForCtorParam("Id", options => options.MapFrom(source => source.Id))
                .ForCtorParam("RouteStops", options => options.MapFrom(source => source.RouteStops
                        .Select(stop => new TransitApplication.AdditionalTypes.RouteStop
                        {
                            BusStopId = stop.BusStop.Id,
                            IntervalToNextStop = stop.IntervalToNextStop
                        })
                        .ToList()));

            CreateMap<Route, RouteRemoved>()
                .ForCtorParam("Id", options => options.MapFrom(source => source.Id))
                .ForCtorParam("RouteStops", options => options.MapFrom(source => source.RouteStops
                        .Select(stop => new TransitApplication.AdditionalTypes.RouteStop
                        {
                            BusStopId = stop.BusStop.Id,
                            IntervalToNextStop = stop.IntervalToNextStop
                        })
                        .ToList()));

            CreateMap<Route, RouteUpdated>()
                .ForCtorParam("Id", options => options.MapFrom(source => source.Id))
                .ForCtorParam("RouteStops", options => options.MapFrom(source => source.RouteStops
                        .Select(stop => new TransitApplication.AdditionalTypes.RouteStop
                        {
                            BusStopId = stop.BusStop.Id,
                            IntervalToNextStop = stop.IntervalToNextStop
                        })
                        .ToList()));
        }
    }
}
