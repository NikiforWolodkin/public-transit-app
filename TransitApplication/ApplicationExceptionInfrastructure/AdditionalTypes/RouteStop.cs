namespace TransitApplication.AdditionalTypes
{
    public class RouteStop
    {
        public Guid BusStopId { get; set; }
        public TimeSpan? IntervalToNextStop { get; set; }
    }
}
