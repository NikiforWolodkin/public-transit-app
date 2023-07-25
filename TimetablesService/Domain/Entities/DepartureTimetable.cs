namespace Domain.Entities
{
    public class DepartureTimetable
    {
        public Guid Id { get; set; }
        public string ActivityIntervalCron { get; set; }
        public List<DateTime> DepartureTimes { get; set; }
    }
}
