using BikeRental.Domain.Enum;

namespace BikeRental.Application.Contracts.Model;
public record ModelDto(
    int Id,
    double? WheelSize,
    double? MaxPassengerWeight,
    double? BikeWeight,
    string? BrakeType,
    int? ModelYear,
    decimal PricePerHour,
    BikeType BikeType
);