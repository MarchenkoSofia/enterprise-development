using BikeRental.Domain;
using BikeRental.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeRental.Infrastructure.EfCore.Repositories;

/// <summary>
/// EF Core repository for managing Renter entities.
/// </summary>
public class RenterEfCoreRepository(BikeRentalDbContext context) : IRepository<Renter, int>
{
    private readonly DbSet<Renter> _renters = context.Renters;

    /// <summary>
    /// Creates a new renter.
    /// </summary>
    public async Task<Renter> Create(Renter entity)
    {
        var result = await _renters.AddAsync(entity);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    /// <summary>
    /// Deletes a renter by ID.
    /// </summary>
    /// <returns>True if deleted, false if not found.</returns>
    public async Task<bool> Delete(int id)
    {
        var entity = await _renters.FirstOrDefaultAsync(e => e.Id == id);
        if (entity == null)
            return false;

        _renters.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Gets a renter by ID.
    /// </summary>
    public async Task<Renter?> Read(int id) =>
        await _renters.FirstOrDefaultAsync(e => e.Id == id);

    /// <summary>
    /// Gets all renters.
    /// </summary>
    public async Task<IList<Renter>> ReadAll() =>
        await _renters.ToListAsync();

    /// <summary>
    /// Updates an existing renter.
    /// </summary>
    public async Task<Renter> Update(Renter entity)
    {
        _renters.Update(entity);
        await context.SaveChangesAsync();
        return (await Read(entity.Id))!;
    }
}
