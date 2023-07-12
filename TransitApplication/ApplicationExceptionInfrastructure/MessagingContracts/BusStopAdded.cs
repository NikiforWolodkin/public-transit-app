using TransitApplication.Enums;

namespace TransitApplication.MessagingContracts
{
    public record BusStopAdded
    (
        Guid Id,
        string Name,
        BusStopType Type
    );
}
