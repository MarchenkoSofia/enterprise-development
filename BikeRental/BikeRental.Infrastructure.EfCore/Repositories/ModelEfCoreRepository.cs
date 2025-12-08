using BikeRental.Domain;
using BikeRental.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeRental.Infrastructure.EfCore.Repositories;

/// <summary>
/// EF Core repository for managing Model entities.
/// </summary>
public class ModelEfCoreRepository(BikeRentalDbContext context) : IRepository<Model, int>
{
    private readonly DbSet<Model> _models = context.Models;

    /// <summary>
    /// Creates a new bike model.
    /// </summary>
    public async Task<Model> Create(Model entity)
    {
        var result = await _models.AddAsync(entity);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    /// <summary>
    /// Deletes a model by ID.
    /// </summary>
    /// <returns>True if deleted, false if not found.</returns>
    public async Task<bool> Delete(int id)
    {
        var entity = await _models.FirstOrDefaultAsync(e => e.Id == id);
        if (entity == null)
            return false;

        _models.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Gets a model by ID.
    /// </summary>
    public async Task<Model?> Read(int id) =>
        await _models.FirstOrDefaultAsync(e => e.Id == id);

    /// <summary>
    /// Gets all bike models.
    /// </summary>
    public async Task<IList<Model>> ReadAll() =>
        await _models.ToListAsync();

    /// <summary>
    /// Updates an existing model.
    /// </summary>
    public async Task<Model> Update(Model entity)
    {
        _models.Update(entity);
        await context.SaveChangesAsync();
        return (await Read(entity.Id))!;
    }
}
