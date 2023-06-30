namespace Services.Dtos
{
    public class RouteAddDto
    {
        public string Name { get; set; }
        public List<RouteStopAddDto> RouteStops { get; set; }
    }
}
