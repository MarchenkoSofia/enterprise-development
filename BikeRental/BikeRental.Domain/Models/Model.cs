using BikeRental.Domain.Enum;

namespace BikeRental.Domain.Models;

/// <summary>
/// A class describing the characteristics of the model.
/// </summary>
public class Model
{
    /// <summary>
    /// Unique identifier of the bike model.
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Wheel size (in inches).
    /// </summary>
    public float? WheelSize { get; set; }

    /// <summary>
    /// Maximum allowed passenger weight (in kg).
    /// </summary>
    public int? MaxPassengerWeight { get; set; }

    /// <summary>
    /// Weight of the bike (in kg).
    /// </summary>
    public float? BikeWeight { get; set; }

    /// <summary>
    /// Type of brakes (e.g., "Disc", "Rim").
    /// </summary>
    public string? BrakeType { get; set; }

    /// <summary>
    /// Model year.
    /// </summary>
    public int? ModelYear { get; set; }

    /// <summary>
    /// Rental price per hour.
    /// </summary>
    public required decimal PricePerHour { get; set; }

    /// <summary>
    /// Type of the bike.
    /// </summary>
    public required BikeType BikeType { get; set; }
}
