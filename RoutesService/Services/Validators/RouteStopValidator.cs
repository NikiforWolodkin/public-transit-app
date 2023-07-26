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
            
            RuleFor(route => route.First().NextRouteStop).NotNull();

            RuleFor(route => route.Last().NextRouteStop).Null();

            RuleFor(route => route.Last().BusStop.Type)
                .Equal(BusStopType.Depo)
                .When(route => route[1].BusStop.Id != route.Last().BusStop.Id);

            RuleFor(route => route.Last().BusStop.Id)
                .Equal(route => route[1].BusStop.Id)
                .When(route => route[1].BusStop.Id == route.Last().BusStop.Id);

            RuleFor(route => route.SkipLast(1)).Must(routeStops =>
            {
                var utilizedBusStopIds = new List<Guid>();

                foreach (var routeStop in routeStops)
                {
                    if (routeStop.NextRouteStop is null)
                    {
                        return false;
                    }

                    if (utilizedBusStopIds.Contains(routeStop.NextRouteStop.BusStop.Id))
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

                    utilizedBusStopIds.Add(routeStop.NextRouteStop.BusStop.Id);
                }

                return true;
            });
        }
    }

}
