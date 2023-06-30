using Domain.Enums;

namespace Domain.Entities
{
    public class BusStop
    {
        public Guid Id { get; set; }
        public BusStopType Type { get; set; }
    }
}
