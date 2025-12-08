namespace BikeRental.Application.Contracts.Model;

/// <summary>
/// Data transfer object representing a bike model.
/// </summary>
/// <param name="Id">Unique identifier of the model.</param>
/// <param name="WheelSize">Diameter of the wheels (e.g., in inches).</param>
/// <param name="MaxPassengerWeight">Maximum supported weight of a passenger.</param>
/// <param name="BikeWeight">Weight of the bike itself.</param>
/// <param name="BrakeType">Type of the braking system.</param>
/// <param name="ModelYear">Year of the model's manufacture.</param>
/// <param name="PricePerHour">Cost of renting this model per hour.</param>
/// <param name="BikeType">Integer representing the bike category (e.g., Sport, Urban).</param>
public record ModelDto(
    int Id,
    double? WheelSize,
    double? MaxPassengerWeight,
    double? BikeWeight,
    string? BrakeType,
    int? ModelYear,
    decimal PricePerHour,
    int BikeType
);
