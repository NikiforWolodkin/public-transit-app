using AutoMapper;
using Domain.Entities;
using Services.Dtos;

namespace Services.Profiles
{
    public class TimetableProfile : Profile
    {
        public TimetableProfile()
        {
            CreateMap<DepartureTimetable, ArrivalsTimetableDto>()
                .ForMember(destination => destination.ArrivalTimes, options => options.MapFrom(
                    source => source.DepartureTimes));

            CreateMap<DepartureTimetableAddDto, DepartureTimetable>()
                .ForMember(destination => destination.Id, options => options.MapFrom(
                    _ => Guid.NewGuid()));
        }
    }
}
