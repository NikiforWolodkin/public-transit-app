using AutoMapper;
using Domain.Entities;
using Domain.Services;
using FluentValidation;
using Serilog;
using Services.Dtos;
using Services.Interfaces;
using TransitApplication.Enums;
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
                var routeStops = await GetRouteStops(routeAddDto.RouteStops);

                var validationResults = await _routeStopValidator.ValidateAsync(routeStops);

                if (!validationResults.IsValid)
                {
                    var error = validationResults.Errors.First();

                    var errorMessage = $"{error.ErrorCode}: {error.ErrorCode}";

                    throw new BadRequestException("Route is invalid.", errorMessage);
                }

                _unitOfWork.AddRoute(route, routeStops);

                await _unitOfWork.SaveChangesAsync();

                route.RouteStops = OrderRouteStops(route.RouteStops);

                var routeDto = _mapper.Map<RouteDto>(route);

                return routeDto;
            }
            catch (BadRequestException ex)
            {
                Log.Information("AddAsync: Route is invalid. Exception => {@ex}", ex);

                throw ex;
            }
            catch (InvalidOperationException ex)
            {
                Log.Information("AddAsync: Bus stop not found. Exception =>  {@ex}", ex);

                throw new NotFoundException("Bus stop not found.", $"{ex.GetType().Name}: {ex.Message}");
            }
            catch (Exception ex)
            {
                Log.Error("AddAsync: Error! Exception =>  {@ex}", ex);

                throw new InternalServerErrorException($"{ex.GetType().Name}: {ex.Message}");
            }
        }

        async Task<ICollection<RouteDto>> IRouteService.GetAllAsync()
        {
            try
            {
                var routesQuryable = await _unitOfWork.GetAllRoutesAsync();
                var routes = routesQuryable.ToList();

                foreach (var route in routes)
                {
                    route.RouteStops = OrderRouteStops(route.RouteStops);
                }

                var routesDto = _mapper.Map<ICollection<RouteDto>>(routes);

                return routesDto;
            }
            catch (Exception ex)
            {
                Log.Error("GetAllAsync: Error! Exception =>  {@ex}", ex);

                throw new InternalServerErrorException($"{ex.GetType().Name}: {ex.Message}");
            }
        }

        async Task<ICollection<RouteDto>> IRouteService.GetBusStopRoutesAsync(Guid busStopId)
        {
            try
            {
                var routesQuryable = await _unitOfWork.GetBusStopRoutesAsync(busStopId);
                var routes = routesQuryable.ToList();

                foreach (var route in routes)
                {
                    route.RouteStops = OrderRouteStops(route.RouteStops);
                }

                var routesDto = _mapper.Map<ICollection<RouteDto>>(routes.ToList());

                return routesDto;
            }
            catch (InvalidOperationException ex)
            {
                Log.Information("GetBusStopRoutesAsync: Bus stop not found. Exception =>  {@ex}", ex);

                throw new NotFoundException("Bus stop not found.", $"{ex.GetType().Name}: {ex.Message}");
            }
            catch (Exception ex)
            {
                Log.Error("GetBusStopRoutesAsync: Error! Exception =>  {@ex}", ex);

                throw new InternalServerErrorException($"{ex.GetType().Name}: {ex.Message}");
            }
        }

        async Task<RouteDto> IRouteService.GetByIdAsync(Guid id)
        {
            try
            {
                var route = await _unitOfWork.GetRouteByIdAsync(id);

                route.RouteStops = OrderRouteStops(route.RouteStops);

                var routeDto = _mapper.Map<RouteDto>(route);

                return routeDto;
            }
            catch (InvalidOperationException ex)
            {
                Log.Information("GetByIdAsync: Route not found. Exception =>  {@ex}", ex);

                throw new NotFoundException("Route not found.", $"{ex.GetType().Name}: {ex.Message}");
            }
            catch (Exception ex)
            {
                Log.Error("GetByIdAsync: Error! Exception =>  {@ex}", ex);

                throw new InternalServerErrorException($"{ex.GetType().Name}: {ex.Message}");
            }
        }

        async Task<ICollection<RouteDto>> IRouteService.GetByRouteNameAsync(string routeName)
        {
            try
            {
                var routesQueryable = await _unitOfWork.GetRoutesByRouteNameAsync(routeName);
                var routes = routesQueryable.ToList();

                foreach (var route in routes)
                {
                    route.RouteStops = OrderRouteStops(route.RouteStops);
                }

                var routesDto = _mapper.Map<ICollection<RouteDto>>(routes.ToList());

                return routesDto;
            }
            catch (Exception ex)
            {
                Log.Error("GetByRouteNameAsync: Error! Exception =>  {@ex}", ex);

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
                Log.Information("RemoveAsync: Route not found. Exception =>  {@ex}", ex);

                throw new NotFoundException("Route not found.", $"{ex.GetType().Name}: {ex.Message}");
            }
            catch (Exception ex)
            {
                Log.Error("RemoveAsync: Error! Exception =>  {@ex}", ex);

                throw new InternalServerErrorException($"{ex.GetType().Name}: {ex.Message}");
            }
        }

        async Task<RouteDto> IRouteService.ReplaceRouteStopsAsync(Guid id, List<RouteStopAddDto> routeStopsDto)
        {
            try
            {
                var route = await _unitOfWork.GetRouteByIdAsync(id);
                var routeStops = await GetRouteStops(routeStopsDto);

                var validationResults = await _routeStopValidator.ValidateAsync(routeStops);

                if (!validationResults.IsValid)
                {
                    var error = validationResults.Errors.First();

                    var errorMessage = $"{error.ErrorCode}: {error.ErrorCode}";

                    throw new BadRequestException("Route is invalid.", errorMessage);
                }

                _unitOfWork.ReplaceRouteStops(route, routeStops);

                await _unitOfWork.SaveChangesAsync();

                route.RouteStops = OrderRouteStops(route.RouteStops);

                var routeDto = _mapper.Map<RouteDto>(route);

                return routeDto;
            }
            catch (BadRequestException ex)
            {
                Log.Information("ReplaceRouteStopsAsync: Route is invalid. Exception =>  {@ex}", ex);

                throw ex;
            }
            catch (InvalidOperationException ex)
            {
                Log.Information("ReplaceRouteStopsAsync: Not found. Exception =>  {@ex}", ex);

                throw new NotFoundException($"{ex.GetType().Name}: {ex.Message}");
            }
            catch (Exception ex)
            {
                Log.Error("ReplaceRouteStopsAsync: Error! Exception =>  {@ex}", ex);

                throw new InternalServerErrorException($"{ex.GetType().Name}: {ex.Message}");
            }
        }

        public async Task<List<RouteStop>> GetRouteStops(List<RouteStopAddDto> routeStopsAddDto)
        {
            var routeStops = new List<RouteStop>();

            foreach (var routeStopAddDto in routeStopsAddDto)
            {
                var busStop = await _unitOfWork.GetBusStopByIdAsync(routeStopAddDto.BusStopId);

                var routeStop = new RouteStop
                {
                    BusStop = busStop,
                    IntervalToNextStop = routeStopAddDto.IntervalToNextStop
                };

                if (routeStops.Count() != 0)
                {
                    routeStops.Last().NextRouteStop = routeStop;
                }

                routeStops.Add(routeStop);
            }

            return routeStops;
        }

        List<RouteStop> OrderRouteStops(ICollection<RouteStop> routeStopsToOrder)
        {
            var routeStops = new List<RouteStop>();

            var firstStop = routeStopsToOrder
                .Where(stop => stop.BusStop.Type == BusStopType.Depo && stop.NextRouteStop is not null)
                .First();

            routeStops.Add(firstStop);

            while (routeStops.Last().NextRouteStop is not null)
            {
                var nextStop = routeStops.Last().NextRouteStop;

                routeStops.Add(nextStop);
            }

            return routeStops;
        }
    }
}