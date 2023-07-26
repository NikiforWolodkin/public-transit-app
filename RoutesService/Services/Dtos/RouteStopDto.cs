namespace Services.Dtos
{
    public class RouteStopDto
    {
        public Guid Id { get; set; }
        public Guid BusStopId { get; set; }
        public TimeSpan? IntervalToNextStop { get; set; }
    }
}
