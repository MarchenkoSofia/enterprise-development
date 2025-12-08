namespace BikeRental.Application.Contracts.Bike;

/// <summary>
/// DTO for creating or updating a bike.
/// </summary>
/// <param name="SerialNumber">Unique serial number of the bike.</param>
/// <param name="Color">Color of the bike frame.</param>
/// <param name="ModelId">Identifier of the associated bike model.</param>
public record BikeCreateUpdateDto(
    string SerialNumber,
    string? Color,
    int ModelId
);
