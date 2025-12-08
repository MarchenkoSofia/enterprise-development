using BikeRental.Domain;
using BikeRental.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeRental.Infrastructure.EfCore.Repositories;

/// <summary>
/// EF Core repository for managing Rent entities.
/// </summary>
public class RentEfCoreRepository(BikeRentalDbContext context) : IRepository<Rent, int>
{
    private readonly DbSet<Rent> _rents = context.Rents;

    /// <summary>
    /// Creates a new rent record.
    /// </summary>
    public async Task<Rent> Create(Rent entity)
    {
        var result = await _rents.AddAsync(entity);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    /// <summary>
    /// Deletes a rent by ID.
    /// </summary>
    /// <returns>True if deleted, false if not found.</returns>
    public async Task<bool> Delete(int id)
    {
        var entity = await _rents
            .Include(r => r.Bike)
            .Include(r => r.Renter)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (entity == null)
            return false;

        _rents.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Gets a rent by ID, including related Bike and Renter data.
    /// </summary>
    public async Task<Rent?> Read(int id) =>
        await _rents
            .Include(r => r.Bike)
            .Include(r => r.Renter)
            .FirstOrDefaultAsync(e => e.Id == id);

    /// <summary>
    /// Gets all rents, including related Bike and Renter data.
    /// </summary>
    public async Task<IList<Rent>> ReadAll() =>
        await _rents
            .Include(r => r.Bike)
            .Include(r => r.Renter)
            .ToListAsync();

    /// <summary>
    /// Updates an existing rent.
    /// </summary>
    public async Task<Rent> Update(Rent entity)
    {
        _rents.Update(entity);
        await context.SaveChangesAsync();
        return (await Read(entity.Id))!;
    }
}
