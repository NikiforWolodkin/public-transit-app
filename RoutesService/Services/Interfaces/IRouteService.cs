using Services.Dtos;

namespace Services.Interfaces
{
    public interface IRouteService
    {
        Task<ICollection<RouteDto>> GetAllAsync();
        Task<ICollection<RouteDto>> GetBusStopRoutesAsync(Guid busStopId);
        Task<ICollection<RouteDto>> GetByRouteNameAsync(string routeName);
        Task<RouteDto> GetByIdAsync(Guid id);
        Task<RouteDto> AddAsync(RouteAddDto routeAddDto);
        Task<RouteDto> ReplaceRouteStopsAsync(Guid id, List<RouteStopAddDto> routeStopsDtos);
        Task RemoveAsync(Guid id);
    }
}
