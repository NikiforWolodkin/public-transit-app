namespace Services.Dtos
{
    public class BusStopTimetableDto
    {
        public Guid RouteId { get; set; }
        public Guid BusStopId { get; set; }
        public List<DateTime> ArrivalsTimetable { get; set; }
    }
}
