namespace Domain.Entities
{
    public class RouteStop
    {
        public Guid Id { get; set; }
        public BusStop BusStop { get; set; }
        public BusStop? NextBusStop { get; set; }
        public TimeSpan? IntervalToNextStop { get; set; }
    }
}
