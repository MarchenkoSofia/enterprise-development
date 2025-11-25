using BikeRental.Domain.Enum;

namespace BikeRental.Application.Contracts.Model;

/// <summary>
/// DTO для модели велосипеда с суммарным временем аренды.
/// </summary>
public record ModelCreateUpdateDto(
    double? WheelSize, 
    double? MaxPassengerWeight,
    double? BikeWeight,
    string? BrakeType,
    int? ModelYear,
    decimal PricePerHour, 
    BikeType BikeType
);
