namespace Domain.Entities
{
    public class Route
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<RouteStop> RouteStops { get; set; }
    }
}
