using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations.Schema;
using TransitApplication.Enums;

namespace Services.Abstractions.Dtos
{
    public class BusStopUpdateDto
    {
        public string Name { get; set; }
        public BusStopType Type { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
