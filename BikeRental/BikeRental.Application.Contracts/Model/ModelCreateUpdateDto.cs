using System.ComponentModel.DataAnnotations;

namespace BikeRental.Application.Contracts.Model;

/// <summary>
/// DTO for creating or updating a bike model.
/// </summary>
/// <param name="WheelSize">Diameter of the wheels (e.g., in inches).</param>
/// <param name="MaxPassengerWeight">Maximum supported passenger weight.</param>
/// <param name="BikeWeight">Weight of the bike itself.</param>
/// <param name="BrakeType">Type of the braking system.</param>
/// <param name="ModelYear">Year of manufacture.</param>
/// <param name="PricePerHour">Rental cost per hour.</param>
/// <param name="BikeType">Category of the bike (value between 1 and 5).</param>
public record ModelCreateUpdateDto(
    double? WheelSize,
    double? MaxPassengerWeight,
    double? BikeWeight,
    string? BrakeType,
    int? ModelYear,
    decimal PricePerHour,

    [Range(1, 5, ErrorMessage = "BikeType must be between 0 and 5.")]
    int? BikeType
);
