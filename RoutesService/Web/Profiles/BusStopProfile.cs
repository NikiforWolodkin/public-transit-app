using AutoMapper;
using Domain.Entities;
using TransitApplication.MessagingContracts;

namespace Web.Profiles
{
    public class BusStopProfile : Profile
    {
        public BusStopProfile()
        {
            CreateMap<BusStopAdded, BusStop>();
            CreateMap<BusStopRemoved, BusStop>();
            CreateMap<BusStopUpdated, BusStop>();
        }
    }
}
