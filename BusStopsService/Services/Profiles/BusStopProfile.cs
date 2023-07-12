using AutoMapper;
using Domain.Entities;
using TransitApplication.MessagingContracts;
using NetTopologySuite.Geometries;
using Services.Abstractions.Dtos;

namespace Services.Profiles
{
    public class BusStopProfile : Profile
    {
        public BusStopProfile()
        {
            CreateMap<BusStopAddDto, BusStop>()
                .ForMember(destination => destination.Coordinates, options => options.MapFrom(
                    source => new Point(source.Longitude, source.Latitude)
                 ));

            CreateMap<BusStop, BusStopDto>()
                .ForMember(destination => destination.Longitude, options => options.MapFrom(
                    source => source.Coordinates.X
                 ))
                .ForMember(destination => destination.Latitude, options => options.MapFrom(
                    source => source.Coordinates.Y
                 ));

            CreateMap<BusStop, BusStopAdded>();
            CreateMap<BusStop, BusStopRemoved>();
            CreateMap<BusStop, BusStopUpdated>();
        }
    }
}
