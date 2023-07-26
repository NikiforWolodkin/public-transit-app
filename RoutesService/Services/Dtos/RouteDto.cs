namespace Services.Dtos
{
    public class RouteDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<RouteStopDto> RouteStops { get; set; }
    }
}
