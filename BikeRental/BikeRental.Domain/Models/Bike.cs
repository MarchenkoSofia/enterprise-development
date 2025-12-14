namespace BikeRental.Domain.Models;

/// <summary>
/// A class describing bike and information about them.
/// </summary>
public class Bike
{
    /// <summary>
    /// Unique identifier of the bike.
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Bike serial number.
    /// </summary>
    public required string SerialNumber { get; set; }

    /// <summary>
    /// The color the bike is painted in.
    /// </summary>
    public string? Color { get; set; }

    /// <summary>
    /// Identifier of the bike model.
    /// </summary>
    public required int ModelId { get; set; }

    /// <summary>
    /// Bike model navigation property.
    /// </summary>
    public virtual Model? Model { get; set; }
}
