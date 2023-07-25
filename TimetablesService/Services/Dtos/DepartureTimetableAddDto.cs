namespace Services.Dtos
{
    public class DepartureTimetableAddDto
    {
        public string ActivityIntervalCron { get; set; }
        public List<DateTime> DepartureTimes { get; set; }
    }
}
