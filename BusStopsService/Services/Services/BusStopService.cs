using AutoMapper;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using MassTransit;
using TransitApplication.MessagingContracts;
using NetTopologySuite.Geometries;
using Services.Abstractions.Dtos;
using Services.Abstractions.Interfaces;
using TransitApplication.HttpExceptions;

namespace Services.Services
{
    internal class BusStopService : IBusStopService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public BusStopService(IRepositoryManager repositoryManager, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        async Task<BusStopDto> IBusStopService.AddAsync(BusStopAddDto busStopAddDto)
        {
            try
            {
                var busStop = _mapper.Map<BusStop>(busStopAddDto);

                _repositoryManager.BusStopRepository.Add(busStop);

                await _repositoryManager.BusStopRepository.SaveChangesAsync();

                var message = _mapper.Map<BusStopAdded>(busStop);
                await _publishEndpoint.Publish(message);

                var busStopDto = _mapper.Map<BusStopDto>(busStop);

                return busStopDto;
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorException($"{ex.GetType().Name}: {ex.Message}");
            }
        }

        async Task<ICollection<BusStopDto>> IBusStopService.GetAllAsync()
        {
            try
            {
                var busStops = await _repositoryManager.BusStopRepository.GetAllAsync();

                var busStopsDto = _mapper.Map<ICollection<BusStopDto>>(busStops);

                return busStopsDto;
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorException($"{ex.GetType().Name}: {ex.Message}");
            }
        }

        async Task<BusStopDto> IBusStopService.GetByIdAsync(Guid id)
        {
            try
            {
                var busStop = await _repositoryManager.BusStopRepository.GetByIdAsync(id);

                var busStopDto = _mapper.Map<BusStopDto>(busStop);

                return busStopDto;
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

        async Task<ICollection<BusStopDto>> IBusStopService.GetNearbyAsync(double longitude, double latitude)
        {
            try
            {
                var location = new Point(longitude, latitude);

                var busStops = await _repositoryManager.BusStopRepository.GetNearbyAsync(location);

                var busStopsDto = _mapper.Map<ICollection<BusStopDto>>(busStops);

                return busStopsDto;
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorException($"{ex.GetType().Name}: {ex.Message}");
            }
        }

        async Task IBusStopService.RemoveAsync(Guid id)
        {
            try
            {
                var busStop = await _repositoryManager.BusStopRepository.GetByIdAsync(id);

                var message = _mapper.Map<BusStopRemoved>(busStop);

                _repositoryManager.BusStopRepository.Remove(busStop);

                await _repositoryManager.BusStopRepository.SaveChangesAsync();

                await _publishEndpoint.Publish(message);
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

        async Task<ICollection<BusStopDto>> IBusStopService.SearchByBusStopNameAsync(string busStopName)
        {
            try
            {
                var busStops = await _repositoryManager.BusStopRepository.SearchByBusStopNameAsync(busStopName);

                var busStopsDto = _mapper.Map<ICollection<BusStopDto>>(busStops);

                return busStopsDto;
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorException($"{ex.GetType().Name}: {ex.Message}");
            }
        }

        async Task<BusStopDto> IBusStopService.UpdateAsync(Guid id, BusStopUpdateDto busStopUpdateDto)
        {
            try
            {
                var busStop = await _repositoryManager.BusStopRepository.GetByIdAsync(id);

                busStop.Name = busStopUpdateDto.Name;
                busStop.Type = busStopUpdateDto.Type;
                busStop.Coordinates.X = busStopUpdateDto.Longitude;
                busStop.Coordinates.Y = busStopUpdateDto.Latitude;

                await _repositoryManager.BusStopRepository.SaveChangesAsync();

                var message = _mapper.Map<BusStopUpdated>(busStop);
                await _publishEndpoint.Publish(message);

                var busStopDto = _mapper.Map<BusStopDto>(busStop);

                return busStopDto;
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
    }
}
