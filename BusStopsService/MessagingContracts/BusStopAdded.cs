using TransitApplication.Enums;

namespace MessagingContracts
{
    public record BusStopAdded
    (
        Guid Id,
        string Name,
        BusStopType Type
    );
}
