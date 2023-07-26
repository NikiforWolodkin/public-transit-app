using TransitApplication.AdditionalTypes;

namespace TransitApplication.MessagingContracts
{
    public record RouteUpdated
    (
        Guid Id,
        List<RouteStop> RouteStops
    );
}
