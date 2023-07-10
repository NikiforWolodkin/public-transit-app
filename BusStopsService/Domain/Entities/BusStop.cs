using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations.Schema;
using TransitApplication.Enums;

namespace Domain.Entities
{

    public class BusStop
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public BusStopType Type { get; set; }
        [Column(TypeName = "geography")]
        public Point Coordinates { get; set; }
    }
}