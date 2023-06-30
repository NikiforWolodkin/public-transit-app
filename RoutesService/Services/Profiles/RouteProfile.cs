using AutoMapper;
using Domain.Entities;
using Services.Dtos;

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
                ))
                .ForMember(destination => destination.NextBusStopId, options => options.MapFrom(
                    source => source.NextBusStop == null ? (Guid?)null : source.NextBusStop.Id
                ));

            CreateMap<RouteAddDto, Route>();

            CreateMap<Route, RouteDto>()
                .ForMember(destination => destination.RouteStops, options => options.MapFrom(
                    source => source.RouteStops
                        .Select(stop => new RouteStopDto
                        {
                            Id = stop.Id,
                            BusStopId = stop.BusStop.Id,
                            NextBusStopId = stop.NextBusStop == null ? null : stop.NextBusStop.Id,
                            IntervalToNextStop = stop.IntervalToNextStop,
                        })
                        .ToList()
                ));
        }
    }
}
