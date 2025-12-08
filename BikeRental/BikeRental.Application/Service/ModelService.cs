using AutoMapper;
using BikeRental.Application.Contracts.Bike;
using BikeRental.Application.Contracts.Model;
using BikeRental.Domain;
using BikeRental.Domain.Models;

namespace BikeRental.Application.Service;

/// <summary>
/// Service responsible for managing bike models.
/// Handles CRUD operations and queries related to bike models and their associated bikes.
/// </summary>
public class ModelService(
    IRepository<Model, int> modelRepository,
    IRepository<Bike, int> bikeRepository,
    IMapper mapper
) : IModelService
{
    /// <summary>
    /// Creates a new bike model.
    /// </summary>
    public async Task<ModelDto> Create(ModelCreateUpdateDto dto)
    {
        var entity = mapper.Map<Model>(dto);
        var res = await modelRepository.Create(entity);
        return mapper.Map<ModelDto>(res);
    }

    /// <summary>
    /// Deletes a bike model by ID.
    /// </summary>
    public async Task<bool> Delete(int id) =>
        await modelRepository.Delete(id);

    /// <summary>
    /// Retrieves a bike model by ID.
    /// </summary>
    public async Task<ModelDto?> Get(int id)
    {
        var entity = await modelRepository.Read(id);
        return mapper.Map<ModelDto>(entity);
    }

    /// <summary>
    /// Retrieves all bike models.
    /// </summary>
    public async Task<IList<ModelDto>> GetAll()
    {
        var all = await modelRepository.ReadAll();
        return mapper.Map<List<ModelDto>>(all);
    }

    /// <summary>
    /// Updates an existing bike model.
    /// </summary>
    public async Task<ModelDto> Update(ModelCreateUpdateDto dto, int id)
    {
        var entity = await modelRepository.Read(id)
            ?? throw new KeyNotFoundException($"Model {id} not found");

        mapper.Map(dto, entity);
        var updated = await modelRepository.Update(entity);
        return mapper.Map<ModelDto>(updated);
    }

    /// <summary>
    /// Retrieves all bikes associated with a specific model.
    /// </summary>
    public async Task<IList<BikeDto>> GetBikesAsync(int modelId)
    {
        var all = await bikeRepository.ReadAll();
        var bikes = all.Where(b => b.ModelId == modelId).ToList();
        return mapper.Map<List<BikeDto>>(bikes);
    }
}
