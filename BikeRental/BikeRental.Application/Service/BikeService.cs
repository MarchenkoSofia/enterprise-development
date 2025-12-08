using AutoMapper;
using BikeRental.Application.Contracts.Bike;
using BikeRental.Domain;
using BikeRental.Domain.Models;

namespace BikeRental.Application.Service;

/// <summary>
/// Service responsible for managing bikes.
/// Handles CRUD operations and queries related to bikes and their models.
/// </summary>
public class BikeService(
    IRepository<Bike, int> bikeRepository,
    IMapper mapper
) : IBikeService
{
    /// <summary>
    /// Creates a new bike.
    /// </summary>
    public async Task<BikeDto> Create(BikeCreateUpdateDto dto)
    {
        var entity = mapper.Map<Bike>(dto);
        var res = await bikeRepository.Create(entity);
        return mapper.Map<BikeDto>(res);
    }

    /// <summary>
    /// Deletes a bike by ID.
    /// </summary>
    public async Task<bool> Delete(int id) =>
        await bikeRepository.Delete(id);

    /// <summary>
    /// Retrieves a bike by ID.
    /// </summary>
    public async Task<BikeDto?> Get(int id)
    {
        var entity = await bikeRepository.Read(id);
        return mapper.Map<BikeDto>(entity);
    }

    /// <summary>
    /// Retrieves all bikes.
    /// </summary>
    public async Task<IList<BikeDto>> GetAll()
    {
        var all = await bikeRepository.ReadAll();
        return mapper.Map<List<BikeDto>>(all);
    }

    /// <summary>
    /// Updates an existing bike.
    /// </summary>
    public async Task<BikeDto> Update(BikeCreateUpdateDto dto, int id)
    {
        var entity = await bikeRepository.Read(id)
            ?? throw new KeyNotFoundException($"Bike {id} not found");

        mapper.Map(dto, entity);
        var updated = await bikeRepository.Update(entity);

        return mapper.Map<BikeDto>(updated);
    }

    /// <summary>
    /// Retrieves all bikes associated with a specific model.
    /// </summary>
    public async Task<IList<BikeDto>> GetBikesByModelAsync(int modelId)
    {
        var all = await bikeRepository.ReadAll();
        var filtered = all.Where(b => b.ModelId == modelId).ToList();
        return mapper.Map<List<BikeDto>>(filtered);
    }



    // public async Task<ModelDto?> GetModelByBikeIdAsync(int bikeId)
    // {
    //     var bike = await bikeRepository.Read(bikeId);
    //     if (bike == null) return null;
    //
    //     var model = await modelRepository.Read(bike.ModelId);
    //     return mapper.Map<ModelDto>(model);
    // }
}
