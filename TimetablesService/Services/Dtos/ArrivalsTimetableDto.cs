namespace Services.Dtos
{
    public class ArrivalsTimetableDto
    {
        public Guid Id { get; set; }
        public string ActivityIntervalCron { get; set; }
        public List<DateTime> ArrivalTimes { get; set; }
    }
}
