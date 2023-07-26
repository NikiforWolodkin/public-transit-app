namespace Domain.Entities
{
    public class RouteStop
    {
        public Guid BusStopId { get; set; }
        public TimeSpan? IntervalToNextStop { get; set; }
    }
}
