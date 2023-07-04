using Domain.Entities;
using TransitApplication.Enums;

namespace Services.Helpers
{
    internal static class RouteStopOrderer
    {
        public static List<RouteStop> Order(ICollection<RouteStop> routeStopsToOrder)
        {
            var routeStops = new List<RouteStop>();

            var depoStop = routeStopsToOrder
                .First(stop => stop.BusStop.Type == BusStopType.Depo && stop.NextBusStop != null);

            routeStops.Add(depoStop);

            while (true)
            {
                var lastStop = routeStops.Last();

                if (lastStop.NextBusStop is not null)
                {
                    var nextStop = routeStopsToOrder.First(stop => lastStop.NextBusStop.Id == stop.BusStop.Id);

                    routeStops.Add(nextStop);

                    continue;
                }

                break;
            }

            return routeStops;
        }
    }
}
