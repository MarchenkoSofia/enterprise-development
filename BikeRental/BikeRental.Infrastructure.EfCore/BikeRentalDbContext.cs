using BikeRental.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeRental.Infrastructure.EfCore;

/// <summary>
/// Database context for the bike rental domain implemented with Entity Framework Core.
/// Configures relationships between entities and defines the data model configuration.
/// </summary>
public class BikeRentalDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Bike> Bikes { get; set; } = null!;
    public DbSet<Model> Models { get; set; } = null!;
    public DbSet<Rent> Rents { get; set; } = null!;
    public DbSet<Renter> Renters { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Bikes
        modelBuilder.Entity<Bike>(builder =>
        {
            builder.HasKey(b => b.Id);

            builder.HasOne(b => b.Model)
                   .WithMany()
                   .HasForeignKey(b => b.ModelId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);
        });

        // Bike models
        modelBuilder.Entity<Model>(builder =>
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.PricePerHour)
                   .HasPrecision(10, 2);
        });

        // Rents
        modelBuilder.Entity<Rent>(builder =>
        {
            builder.HasKey(r => r.Id);

            builder.HasOne(r => r.Bike)
                   .WithMany()
                   .HasForeignKey(r => r.BikeId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Renter)
                   .WithMany()
                   .HasForeignKey(r => r.RenterId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);
        });

        // Renters
        modelBuilder.Entity<Renter>(builder =>
        {
            builder.HasKey(r => r.Id);

            builder.HasIndex(r => r.PhoneNumber)
                   .IsUnique();

            builder.Property(r => r.PhoneNumber)
                   .HasMaxLength(20);

            builder.Property(r => r.LastName)
                   .HasMaxLength(100);

            builder.Property(r => r.Name)
                   .HasMaxLength(100);

            builder.Property(r => r.MiddleName)
                   .HasMaxLength(100);
        });
    }
}
