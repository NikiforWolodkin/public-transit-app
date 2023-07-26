using NetTopologySuite.Geometries;
using TransitApplication.Enums;

namespace Services.Abstractions.Dtos
{
    public class BusStopDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public BusStopType Type { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
