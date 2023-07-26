namespace Domain.Entities
{
    public class RouteStop
    {
        public Guid Id { get; set; }
        public virtual BusStop BusStop { get; set; }
        public virtual RouteStop? NextRouteStop { get; set; }
        public TimeSpan? IntervalToNextStop { get; set; }
    }
}
