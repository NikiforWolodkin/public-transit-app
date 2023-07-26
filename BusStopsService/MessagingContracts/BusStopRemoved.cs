using TransitApplication.Enums;

namespace MessagingContracts
{
    public record BusStopRemoved
    (
        Guid Id,
        string Name,
        BusStopType Type
    );
}
