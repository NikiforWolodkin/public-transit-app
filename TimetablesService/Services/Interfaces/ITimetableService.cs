using Services.Dtos;

namespace Services.Interfaces
{
    public interface ITimetableService
    {
        Task UpdateAsync(Guid routeId, TimetableUpdateDto timetableUpdateDto);
        Task<BusStopTimetableDto> GetBusStopTimetableAsync(Guid routeId, Guid busStopId);
    }
}
