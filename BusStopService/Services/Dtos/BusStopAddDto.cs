using Domain.Enums;
using NetTopologySuite.Geometries;

namespace Services.Abstractions.Dtos
{
    public class BusStopAddDto
    {
        public string Name { get; set; }
        public BusStopType Type { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
