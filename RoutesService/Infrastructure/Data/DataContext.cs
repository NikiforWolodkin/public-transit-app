using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DbSet<BusStop> BusStops { get; set; }
        public DbSet<RouteStop> RouteStops { get; set; }
        public DbSet<Route> Routes { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    }
}
