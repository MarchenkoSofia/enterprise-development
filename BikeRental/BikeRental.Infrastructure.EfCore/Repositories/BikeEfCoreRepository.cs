using BikeRental.Domain;
using BikeRental.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeRental.Infrastructure.EfCore.Repositories;

/// <summary>
/// EF Core repository for managing Bike entities.
/// </summary>
public class BikeEfCoreRepository(BikeRentalDbContext context) : IRepository<Bike, int>
{
    private readonly DbSet<Bike> _bikes = context.Bikes;

    /// <summary>
    /// Creates a new bike.
    /// </summary>
    public async Task<Bike> Create(Bike entity)
    {
        var result = await _bikes.AddAsync(entity);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    /// <summary>
    /// Deletes a bike by ID.
    /// </summary>
    /// <returns>True if deleted, false if not found.</returns>
    public async Task<bool> Delete(int id)
    {
        var entity = await _bikes
            .Include(b => b.Model)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (entity == null)
            return false;

        _bikes.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Gets a bike by ID, including its Model.
    /// </summary>
    public async Task<Bike?> Read(int id) =>
        await _bikes
            .Include(b => b.Model)
            .FirstOrDefaultAsync(e => e.Id == id);

    /// <summary>
    /// Gets all bikes, including their Models.
    /// </summary>
    public async Task<IList<Bike>> ReadAll() =>
        await _bikes
            .Include(b => b.Model)
            .ToListAsync();

    /// <summary>
    /// Updates an existing bike.
    /// </summary>
    public async Task<Bike> Update(Bike entity)
    {
        _bikes.Update(entity);
        await context.SaveChangesAsync();
        return (await Read(entity.Id))!;
    }
}
