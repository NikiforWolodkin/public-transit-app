using AutoMapper;
using Domain.Entities;
using Domain.Services;
using Services.Dtos;
using Services.Helpers;
using Services.Interfaces;
using TransitApplication.HttpExceptions;

namespace Services.Services
{
    public class RouteService : IRouteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RouteService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        async Task<RouteDto> IRouteService.AddAsync(RouteAddDto routeAddDto)
        {
            try
            {   
                var route = _mapper.Map<Route>(routeAddDto);
                var routeStops = await RouteStopMapper
                    .MapFromDtoAsync(routeAddDto.RouteStops, _unitOfWork);

                _unitOfWork.AddRoute(route, routeStops);

                await _unitOfWork.SaveChangesAsync();

                var routeDto = _mapper.Map<RouteDto>(route);

                return routeDto;
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorException($"{ex.GetType().Name}: {ex.Message}");
            }
        }

        async Task<ICollection<RouteDto>> IRouteService.GetAllAsync()
        {
            try
            {
                var routes = await _unitOfWork.GetAllRoutesAsync();

                var routesDto = _mapper.Map<ICollection<RouteDto>>(routes);

                return routesDto;
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorException($"{ex.GetType().Name}: {ex.Message}");
            }
        }

        async Task<ICollection<RouteDto>> IRouteService.GetBusStopRoutesAsync(Guid busStopId)
        {
            try
            {
                var routes = await _unitOfWork.GetBusStopRoutesAsync(busStopId);

                var routesDto = _mapper.Map<ICollection<RouteDto>>(routes);

                return routesDto;
            }
            catch (InvalidOperationException ex)
            {
                throw new NotFoundException("Bus stop not found", $"{ex.GetType().Name}: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorException($"{ex.GetType().Name}: {ex.Message}");
            }
        }

        async Task<RouteDto> IRouteService.GetByIdAsync(Guid id)
        {
            try
            {
                var route = await _unitOfWork.GetRouteByIdAsync(id);

                var routeDto = _mapper.Map<RouteDto>(route);

                return routeDto;
            }
            catch (InvalidOperationException ex)
            {
                throw new NotFoundException("Route not found", $"{ex.GetType().Name}: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorException($"{ex.GetType().Name}: {ex.Message}");
            }
        }

        async Task<ICollection<RouteDto>> IRouteService.GetByRouteNameAsync(string routeName)
        {
            try
            {
                var routes = await _unitOfWork.GetRoutesByRouteNameAsync(routeName);

                var routesDto = _mapper.Map<ICollection<RouteDto>>(routes);

                return routesDto;
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorException($"{ex.GetType().Name}: {ex.Message}");
            }
        }

        async Task IRouteService.RemoveAsync(Guid id)
        {
            try
            {
                var route = await _unitOfWork.GetRouteByIdAsync(id);

                _unitOfWork.RemoveRoute(route);

                await _unitOfWork.SaveChangesAsync();
            }
            catch (InvalidOperationException ex)
            {
                throw new NotFoundException("Route not found", $"{ex.GetType().Name}: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorException($"{ex.GetType().Name}: {ex.Message}");
            }
        }

        async Task<RouteDto> IRouteService.ReplaceRouteStopsAsync(Guid id, List<RouteStopAddDto> routeStopsDto)
        {
            try
            {
                var route = await _unitOfWork.GetRouteByIdAsync(id);
                var routeStops = await RouteStopMapper
                    .MapFromDtoAsync(routeStopsDto, _unitOfWork);

                _unitOfWork.ReplaceRouteStops(route, routeStops);

                await _unitOfWork.SaveChangesAsync();

                var routeDto = _mapper.Map<RouteDto>(route);

                return routeDto;
            }
            catch (InvalidOperationException ex)
            {
                throw new NotFoundException("Route not found", $"{ex.GetType().Name}: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorException($"{ex.GetType().Name}: {ex.Message}");
            }
        }
    }
}