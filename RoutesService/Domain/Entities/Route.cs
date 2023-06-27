namespace Domain.Entities
{
    public class Route
    {
        public Guid Id { get; set; }
        public string RouteName { get; set; }
        public ICollection<RouteStop> Stops { get; set; }
    }
}
