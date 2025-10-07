namespace BikeRental.Domain.Models;

/// <summary>
/// Represents the type of a bike.
/// </summary>
public enum BikeType
{
    /// <summary>
    /// Designed for road riding.
    /// </summary>
    Road,

    /// <summary>
    /// Built for off-road terrain.
    /// </summary>
    Mountain,

    /// <summary>
    /// Made for high-speed racing.
    /// </summary>
    Speed,

    /// <summary>
    /// Used for track cycling.
    /// </summary>
    Track,

    /// <summary>
    /// Features electric motor assistance.
    /// </summary>
    Electric,

    /// <summary>
    /// Child-sized bike.
    /// </summary>
    Mini
}