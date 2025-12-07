using AutoMapper;
using BikeRental.Application.Contracts.Rent;
using BikeRental.Domain;
using BikeRental.Domain.Models;


namespace BikeRental.Application.Service;
public class RentService(
    IRepository<Rent, int> rentRepository,
    IMapper mapper
) : IRentService
{
    public async Task<RentDto> Create(RentCreateUpdateDto dto)
    {
        var entity = mapper.Map<Rent>(dto);
        var res = await rentRepository.Create(entity);
        return mapper.Map<RentDto>(res);
    }

    public async Task<bool> Delete(int id) =>
        await rentRepository.Delete(id);

    public async Task<RentDto?> Get(int id)
    {
        var entity = await rentRepository.Read(id);
        return mapper.Map<RentDto>(entity);
    }

    public async Task<IList<RentDto>> GetAll()
    {
        var all = await rentRepository.ReadAll();
        return mapper.Map<List<RentDto>>(all);
    }

    public async Task<RentDto> Update(RentCreateUpdateDto dto, int id)
    {
        var entity = await rentRepository.Read(id)
            ?? throw new KeyNotFoundException($"Rent {id} not found");

        mapper.Map(dto, entity);
        var updated = await rentRepository.Update(entity);

        return mapper.Map<RentDto>(updated);
    }

    public async Task<IList<RentDto>> GetRentsByBikeAsync(int bikeId)
    {
        var all = await rentRepository.ReadAll();
        return mapper.Map<List<RentDto>>(all.Where(r => r.BikeId == bikeId).ToList());
    }

    public async Task<IList<RentDto>> GetRentsByRenterAsync(int renterId)
    {
        var all = await rentRepository.ReadAll();
        return mapper.Map<List<RentDto>>(all.Where(r => r.RenterId == renterId).ToList());
    }

    public async Task<IList<KeyValuePair<int, IList<RentDto>>>> GetRentsGroupedByModelAsync()
    {
        var all = await rentRepository.ReadAll();

        var grouped = all
            .GroupBy(r => r.BikeId) 
            .Select(g => new KeyValuePair<int, IList<RentDto>>(
                g.Key,
                mapper.Map<List<RentDto>>(g.ToList())
            ))
            .ToList();

        return grouped;
    }
}
