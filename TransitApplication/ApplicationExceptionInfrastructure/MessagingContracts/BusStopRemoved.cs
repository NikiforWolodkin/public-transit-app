using TransitApplication.Enums;

namespace TransitApplication.MessagingContracts
{
    public record BusStopRemoved
    (
        Guid Id,
        string Name,
        BusStopType Type
    );
}
