using BikeRental.Domain;
using BikeRental.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeRental.Infrastructure.EfCore.Repositories;

public class RentEfCoreRepository(BikeRentalDbContext context) : IRepository<Rent, int>
{
    private readonly DbSet<Rent> _rents = context.Rents;

    public async Task<Rent> Create(Rent entity)
    {
        var result = await _rents.AddAsync(entity);
        await context.SaveChangesAsync();
        return result.Entity;
    }

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

    public async Task<Rent?> Read(int id) =>
        await _rents
            .Include(r => r.Bike)
            .Include(r => r.Renter)
            .FirstOrDefaultAsync(e => e.Id == id);

    public async Task<IList<Rent>> ReadAll() =>
        await _rents
            .Include(r => r.Bike)
            .Include(r => r.Renter)
            .ToListAsync();

    public async Task<Rent> Update(Rent entity)
    {
        _rents.Update(entity);
        await context.SaveChangesAsync();
        return (await Read(entity.Id))!;
    }
}
