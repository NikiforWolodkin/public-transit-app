using TransitApplication.AdditionalTypes;

namespace TransitApplication.MessagingContracts
{
    public record RouteRemoved
    (
        Guid Id,
        List<RouteStop> RouteStops
    );
}
