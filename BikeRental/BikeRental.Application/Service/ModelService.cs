using AutoMapper;
using BikeRental.Application.Contracts.Bike;
using BikeRental.Application.Contracts.Model;
using BikeRental.Domain;
using BikeRental.Domain.Models;

public class ModelService(
    IRepository<Model, int> modelRepository,
    IRepository<Bike, int> bikeRepository,
    IMapper mapper
) : IModelService
{
    public async Task<ModelDto> Create(ModelCreateUpdateDto dto)
    {
        var entity = mapper.Map<Model>(dto);
        var res = await modelRepository.Create(entity);
        return mapper.Map<ModelDto>(res);
    }

    public async Task<bool> Delete(int id) =>
        await modelRepository.Delete(id);

    public async Task<ModelDto?> Get(int id)
    {
        var entity = await modelRepository.Read(id);
        return mapper.Map<ModelDto>(entity);
    }

    public async Task<IList<ModelDto>> GetAll()
    {
        var all = await modelRepository.ReadAll();
        return mapper.Map<List<ModelDto>>(all);
    }

    public async Task<ModelDto> Update(ModelCreateUpdateDto dto, int id)
    {
        var entity = await modelRepository.Read(id)
            ?? throw new KeyNotFoundException($"Model {id} not found");

        mapper.Map(dto, entity);
        var updated = await modelRepository.Update(entity);
        return mapper.Map<ModelDto>(updated);
    }

    public async Task<IList<BikeDto>> GetBikesAsync(int modelId)
    {
        var all = await bikeRepository.ReadAll();
        var bikes = all.Where(b => b.ModelId == modelId).ToList();
        return mapper.Map<List<BikeDto>>(bikes);
    }
}
