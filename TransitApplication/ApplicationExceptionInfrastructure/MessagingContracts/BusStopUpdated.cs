using TransitApplication.Enums;

namespace TransitApplication.MessagingContracts
{
    public record BusStopUpdated
    (
        Guid Id,
        string Name,
        BusStopType Type
    );
}
