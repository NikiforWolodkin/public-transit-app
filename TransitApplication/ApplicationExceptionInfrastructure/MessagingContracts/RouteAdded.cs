using TransitApplication.AdditionalTypes;

namespace TransitApplication.MessagingContracts
{
    public record RouteAdded
    (
        Guid Id,
        List<RouteStop> RouteStops
    );
}
