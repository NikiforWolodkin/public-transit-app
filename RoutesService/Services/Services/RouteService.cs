using AutoMapper;
using Domain.Entities;
using Domain.Services;
using FluentValidation;
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
        private readonly IValidator<List<RouteStop>> _routeStopValidator;

        public RouteService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<List<RouteStop>> routeStopValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _routeStopValidator = routeStopValidator;
        }

        async Task<RouteDto> IRouteService.AddAsync(RouteAddDto routeAddDto)
        {
            try
            {   
                var route = _mapper.Map<Route>(routeAddDto);
                var routeStops = await RouteStopMapper
                    .MapFromDtoAsync(routeAddDto.RouteStops, _unitOfWork);

                var validationResults = await _routeStopValidator.ValidateAsync(routeStops);

                if (!validationResults.IsValid)
                {
                    var error = validationResults.Errors.First();

                    var errorMessage = $"{error.ErrorCode}: {error.ErrorCode}";

                    throw new BadRequestException("Route is invalid.", errorMessage);
                }

                _unitOfWork.AddRoute(route, routeStops);

                await _unitOfWork.SaveChangesAsync();

                var routeDto = _mapper.Map<RouteDto>(route);

                return routeDto;
            }
            catch (BadRequestException ex)
            {
                throw ex;
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

        async Task<ICollection<RouteDto>> IRouteService.GetAllAsync()
        {
            try
            {
                var routes = await _unitOfWork.GetAllRoutesAsync();

                foreach (var route in routes)
                {
                    route.RouteStops = RouteStopOrderer.Order(route.RouteStops);
                }

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

                foreach (var route in routes)
                {
                    route.RouteStops = RouteStopOrderer.Order(route.RouteStops);
                }

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

                route.RouteStops = RouteStopOrderer.Order(route.RouteStops);

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

                foreach (var route in routes)
                {
                    route.RouteStops = RouteStopOrderer.Order(route.RouteStops);
                }

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

                var validationResults = await _routeStopValidator.ValidateAsync(routeStops);

                if (!validationResults.IsValid)
                {
                    var error = validationResults.Errors.First();

                    var errorMessage = $"{error.ErrorCode}: {error.ErrorCode}";

                    throw new BadRequestException("Route is invalid.", errorMessage);
                }

                _unitOfWork.ReplaceRouteStops(route, routeStops);

                await _unitOfWork.SaveChangesAsync();

                var routeDto = _mapper.Map<RouteDto>(route);

                return routeDto;
            }
            catch (BadRequestException ex)
            {
                throw ex;
            }
            catch (InvalidOperationException ex)
            {
                throw new NotFoundException($"{ex.GetType().Name}: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorException($"{ex.GetType().Name}: {ex.Message}");
            }
        }
    }
}