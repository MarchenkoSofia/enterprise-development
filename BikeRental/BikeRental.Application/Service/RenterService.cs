using AutoMapper;
using BikeRental.Application.Contracts.Rent;
using BikeRental.Application.Contracts.Renter;
using BikeRental.Domain;
using BikeRental.Domain.Models;

namespace BikeRental.Application.Service;

/// <summary>
/// Service responsible for managing renters (customers).
/// Handles CRUD operations and queries related to renters and their rental history.
/// </summary>
public class RenterService(
    IRepository<Renter, int> renterRepository,
    IRepository<Rent, int> rentRepository,
    IMapper mapper
) : IRenterService
{
    /// <summary>
    /// Creates a new renter.
    /// </summary>
    public async Task<RenterDto> Create(RenterCreateUpdateDto dto)
    {
        var entity = mapper.Map<Renter>(dto);
        var res = await renterRepository.Create(entity);
        return mapper.Map<RenterDto>(res);
    }

    /// <summary>
    /// Deletes a renter by ID.
    /// </summary>
    public async Task<bool> Delete(int id) =>
        await renterRepository.Delete(id);

    /// <summary>
    /// Retrieves a renter by ID.
    /// </summary>
    public async Task<RenterDto?> Get(int id)
    {
        var entity = await renterRepository.Read(id);
        return mapper.Map<RenterDto>(entity);
    }

    /// <summary>
    /// Retrieves all renters.
    /// </summary>
    public async Task<IList<RenterDto>> GetAll()
    {
        var all = await renterRepository.ReadAll();
        return mapper.Map<List<RenterDto>>(all);
    }

    /// <summary>
    /// Updates an existing renter.
    /// </summary>
    public async Task<RenterDto> Update(RenterCreateUpdateDto dto, int id)
    {
        var entity = await renterRepository.Read(id)
            ?? throw new KeyNotFoundException($"Renter {id} not found");

        mapper.Map(dto, entity);
        var updated = await renterRepository.Update(entity);

        return mapper.Map<RenterDto>(updated);
    }

    /// <summary>
    /// Retrieves all rentals associated with a specific renter.
    /// </summary>
    public async Task<IList<RentDto>> GetRenterRentsAsync(int renterId)
    {
        var rents = await rentRepository.ReadAll();
        var filtered = rents.Where(r => r.RenterId == renterId).ToList();
        return mapper.Map<List<RentDto>>(filtered);
    }
}
