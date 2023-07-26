using MongoDB.Bson;

namespace Domain.Entities
{
    public class RouteTimetable
    {
        public ObjectId Id { get; set; }
        public Guid RouteId { get; set; }
        public List<DepartureTimetable>? DepartureTimetables { get; set; }
        public List<RouteStop> RouteStops { get; set; }
    }
}
