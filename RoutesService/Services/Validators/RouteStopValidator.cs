using Domain.Entities;
using FluentValidation;
using TransitApplication.Enums;

namespace Services.Validators
{
    public class RouteStopValidator : AbstractValidator<List<RouteStop>>
    {
        public RouteStopValidator()
        {
            RuleFor(route => route).NotNull();

            RuleFor(route => route.Count).GreaterThanOrEqualTo(2);

            RuleFor(route => route.First().BusStop.Type).Equal(BusStopType.Depo);
            
            RuleFor(route => route.First().NextBusStop).NotNull();

            RuleFor(route => route.Last().BusStop.Type)
                .Equal(BusStopType.Depo)
                .When(stop => stop.Last().NextBusStop is null);

            RuleFor(route => route.Last().NextBusStop.Id)
                .Equal(stop => stop[1].BusStop.Id)
                .When(route => route.Last().NextBusStop is not null);

            RuleFor(route => route.SkipLast(1)).Must(routeStops =>
            {
                var utilizedBusStopIds = new List<Guid>();

                foreach (var routeStop in routeStops.SkipLast(1))
                {
                    if (routeStop.NextBusStop is null)
                    {
                        return false;
                    }

                    if (utilizedBusStopIds.Contains(routeStop.NextBusStop.Id))
                    {
                        return false;
                    }

                    if (utilizedBusStopIds.Count() != 0)
                    {
                        if (utilizedBusStopIds.Last() != routeStop.BusStop.Id)
                        {
                            return false;
                        }
                    }

                    utilizedBusStopIds.Add(routeStop.NextBusStop.Id);
                }

                return true;
            });
        }
    }

}
