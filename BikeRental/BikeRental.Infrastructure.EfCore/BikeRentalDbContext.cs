using BikeRental.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace BikeRental.Infrastructure.EfCore;

/// <summary>
/// Контекст базы данных для аренды велосипедов, реализующий Entity Framework Core.
/// Настраивает связи между сущностями и задаёт конфигурацию модели данных.
/// </summary>
public class BikeRentalDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Bike> Bikes { get; set; } = null!;
    public DbSet<Model> Models { get; set; } = null!;
    public DbSet<Rent> Rents { get; set; } = null!;
    public DbSet<Renter> Renters { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Велосипеды
        modelBuilder.Entity<Bike>(builder =>
        {
            builder.HasKey(b => b.Id);

            builder.HasOne(b => b.Model)
                   .WithMany(m => m.Bikes)
                   .IsRequired();

            builder.HasMany(b => b.Rents)
                   .WithOne(r => r.Bike)
                   .IsRequired();
        });

        // Модели велосипедов
        modelBuilder.Entity<Model>(builder =>
        {
            builder.HasKey(m => m.Id);

            builder.HasMany(m => m.Bikes)
                   .WithOne(b => b.Model)
                   .IsRequired();
        
       
        });

        // Аренды
        modelBuilder.Entity<Rent>(builder =>
        {
            builder.HasKey(r => r.Id);

            builder.HasOne(r => r.Bike)
                   .WithMany(b => b.Rents)
                   .IsRequired();

            builder.HasOne(r => r.Renter)
                   .WithMany(r => r.Rents)
                   .IsRequired();
        });

        // Арендаторы
        modelBuilder.Entity<Renter>(builder =>
        {
            builder.HasKey(r => r.Id);

            builder.HasIndex(r => r.PhoneNumber).IsUnique();

            builder.HasMany(r => r.Rents)
                   .WithOne(rent => rent.Renter);
        });
    }
}
