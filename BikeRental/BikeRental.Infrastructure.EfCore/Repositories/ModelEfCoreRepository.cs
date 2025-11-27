using BikeRental.Domain;
using BikeRental.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeRental.Infrastructure.EfCore.Repositories;

public class ModelEfCoreRepository(BikeRentalDbContext context) : IRepository<Model, int>
{
    private readonly DbSet<Model> _models = context.Models;

    public async Task<Model> Create(Model entity)
    {
        var result = await _models.AddAsync(entity);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<bool> Delete(int id)
    {
        var entity = await _models.FirstOrDefaultAsync(e => e.Id == id);
        if (entity == null)
            return false;

        _models.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<Model?> Read(int id) =>
        await _models.FirstOrDefaultAsync(e => e.Id == id);

    public async Task<IList<Model>> ReadAll() =>
        await _models.ToListAsync();

    public async Task<Model> Update(Model entity)
    {
        _models.Update(entity);
        await context.SaveChangesAsync();
        return (await Read(entity.Id))!;
    }
}
