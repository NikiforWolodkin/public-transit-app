using TransitApplication.Enums;

namespace MessagingContracts
{
    public record BusStopUpdated
    (
        Guid Id,
        string Name,
        BusStopType Type
    );
}
