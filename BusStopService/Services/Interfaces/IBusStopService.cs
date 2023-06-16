using Services.Abstractions.Dtos;

namespace Services.Abstractions.Interfaces
{
    public interface IBusStopService
    {
        /// <summary>
        /// Gets all bus stops asynchronously.
        /// </summary>
        /// <returns>A collection of BusStopDto objects.</returns>
        /// <exception cref="Domain.Exceptions.InternalServerErrorException">Thrown when an internal server error occurs.</exception>
        Task<ICollection<BusStopDto>> GetAllAsync();

        /// <summary>
        /// Gets nearby bus stops asynchronously based on the provided longitude and latitude.
        /// </summary>
        /// <param name="longitude">The longitude of the location.</param>
        /// <param name="latitude">The latitude of the location.</param>
        /// <returns>A collection of BusStopDto objects.</returns>
        /// <exception cref="Domain.Exceptions.InternalServerErrorException">Thrown when an internal server error occurs.</exception>
        Task<ICollection<BusStopDto>> GetNearbyAsync(double longitude, double latitude);

        /// <summary>
        /// Gets a bus stop by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the bus stop.</param>
        /// <returns>A BusStopDto object.</returns>
        /// <exception cref="Domain.Exceptions.NotFoundException">Thrown when a bus stop with specified id is not found.</exception>
        /// <exception cref="Domain.Exceptions.InternalServerErrorException">Thrown when an internal server error occurs.</exception>
        Task<BusStopDto> GetByIdAsync(Guid id);

        /// <summary>
        /// Adds a new bus stop asynchronously.
        /// </summary>
        /// <param name="busStopAddDto">The data transfer object containing the information for the new bus stop.</param>
        /// <returns>A BusStopDto object representing the added bus stop.</returns>
        /// <exception cref="Domain.Exceptions.InternalServerErrorException">Thrown when an internal server error occurs.</exception>
        Task<BusStopDto> AddAsync(BusStopAddDto busStopAddDto);

        /// <summary>
        /// Updates an existing bus stop asynchronously.
        /// </summary>
        /// <param name="id">The ID of the bus stop to update.</param>
        /// <param name="busStopUpdateDto">The data transfer object containing the updated information for the bus stop.</param>
        /// <returns>A BusStopDto object representing the updated bus stop.</returns>
        /// <exception cref="Domain.Exceptions.NotFoundException">Thrown when a bus stop with specified id is not found.</exception>
        /// <exception cref="Domain.Exceptions.InternalServerErrorException">Thrown when an internal server error occurs.</exception>
        Task<BusStopDto> UpdateAsync(Guid id, BusStopUpdateDto busStopUpdateDto);

        /// <summary>
        /// Removes a bus stop asynchronously.
        /// </summary>
        /// <param name="id">The ID of the bus stop to remove.</param>
        /// <exception cref="Domain.Exceptions.NotFoundException">Thrown when a bus stop with specified id is not found.</exception>
        /// <exception cref="Domain.Exceptions.InternalServerErrorException">Thrown when an internal server error occurs.</exception>
        Task RemoveAsync(Guid id);
    }
}
