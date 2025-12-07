namespace BikeRental.Domain.Models;

/// <summary>
/// The class describing the fact of renting a bike by a renter.
/// </summary>
public class Rent
{
    /// <summary>
    /// Unique identifier of the rent.
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Start time of the rental.
    /// </summary>
    public required DateTime StartTime { get; set; }

    /// <summary>
    /// Duration of the rental (in hours).
    /// </summary>
    public required int Duration { get; set; }

    /// <summary>
    /// Unique identifier of the bike.
    /// </summary>
    public required int BikeId { get; set; }

    /// <summary>
    /// Bike navigation property for Rent.
    /// </summary>
    public required virtual Bike Bike { get; set; }

    /// <summary>
    /// Unique identifier of the renter.
    /// </summary>
    public required int RenterId { get; set; }

    /// <summary>
    /// Renter to whom it is Rent.
    /// </summary>
    public required virtual Renter Renter { get; set; }
}
