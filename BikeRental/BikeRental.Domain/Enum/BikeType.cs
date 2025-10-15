namespace BikeRental.Domain.Enum;

/// <summary>
///     Represents the type of bike.
/// </summary>
public enum BikeType
{
    /// <summary>
    ///     Designed for road riding.
    /// </summary>
    City,

    /// <summary>
    ///     Built for off-road terrain.
    /// </summary>
    Mountain,

    /// <summary>
    ///     Made for high-speed sport racing.
    /// </summary>
    Sport,

    /// <summary>
    ///     Used for track cycling.
    /// </summary>
    Track,

    /// <summary>
    ///     Features electric motor assistance.
    /// </summary>
    Electric,

    /// <summary>
    ///     Child-sized bike.
    /// </summary>
    Mini
}