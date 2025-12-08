using AutoMapper;
using BikeRental.Application.Contracts.Rent;
using BikeRental.Domain;
using BikeRental.Domain.Models;

namespace BikeRental.Application.Service;

/// <summary>
/// Service responsible for managing bike rentals.
/// Handles CRUD operations and specific queries related to rent records.
/// </summary>
public class RentService(
    IRepository<Rent, int> rentRepository,
    IMapper mapper
) : IRentService
{
    /// <summary>
    /// Creates a new rental record.
    /// </summary>
    public async Task<RentDto> Create(RentCreateUpdateDto dto)
    {
        var entity = mapper.Map<Rent>(dto);
        var res = await rentRepository.Create(entity);
        return mapper.Map<RentDto>(res);
    }

    /// <summary>
    /// Deletes a rent record by its identifier.
    /// </summary>
    public async Task<bool> Delete(int id) =>
        await rentRepository.Delete(id);

    /// <summary>
    /// Retrieves a specific rent record by its identifier.
    /// </summary>
    public async Task<RentDto?> Get(int id)
    {
        var entity = await rentRepository.Read(id);
        return mapper.Map<RentDto>(entity);
    }

    /// <summary>
    /// Retrieves all rent records.
    /// </summary>
    public async Task<IList<RentDto>> GetAll()
    {
        var all = await rentRepository.ReadAll();
        return mapper.Map<List<RentDto>>(all);
    }

    /// <summary>
    /// Updates an existing rent record.
    /// </summary>
    public async Task<RentDto> Update(RentCreateUpdateDto dto, int id)
    {
        var entity = await rentRepository.Read(id)
            ?? throw new KeyNotFoundException($"Rent {id} not found");

        mapper.Map(dto, entity);
        var updated = await rentRepository.Update(entity);

        return mapper.Map<RentDto>(updated);
    }

    /// <summary>
    /// Retrieves all rentals associated with a specific bike.
    /// </summary>
    public async Task<IList<RentDto>> GetRentsByBikeAsync(int bikeId)
    {
        var all = await rentRepository.ReadAll();
        return mapper.Map<List<RentDto>>(all.Where(r => r.BikeId == bikeId).ToList());
    }

    /// <summary>
    /// Retrieves all rentals made by a specific renter.
    /// </summary>
    public async Task<IList<RentDto>> GetRentsByRenterAsync(int renterId)
    {
        var all = await rentRepository.ReadAll();
        return mapper.Map<List<RentDto>>(all.Where(r => r.RenterId == renterId).ToList());
    }

}
