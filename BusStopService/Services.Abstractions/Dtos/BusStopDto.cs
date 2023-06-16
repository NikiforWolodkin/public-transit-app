using Domain.Enums;
using System.Drawing;

namespace Services.Abstractions.Dtos
{
    public class BusStopDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public BusStopType Type { get; set; }
        public Point Coordinates { get; set; }
    }
}
