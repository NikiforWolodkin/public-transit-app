namespace Services.Dtos
{
    public class RouteStopAddDto
    {
        public Guid BusStopId { get; set; }
        public Guid? NextBusStopId { get; set; }
        public TimeSpan? IntervalToNextStop { get; set; }
    }
}
