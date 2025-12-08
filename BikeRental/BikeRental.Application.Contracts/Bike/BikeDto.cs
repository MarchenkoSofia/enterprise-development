namespace BikeRental.Application.Contracts.Bike;

/// <summary>
/// Data transfer object representing a bike.
/// </summary>
/// <param name="Id">Unique identifier of the bike.</param>
/// <param name="SerialNumber">Unique serial number assigned to the bike.</param>
/// <param name="Color">Color of the bike frame.</param>
/// <param name="ModelId">Identifier of the bike's model.</param>
public record BikeDto(
    int Id,
    string SerialNumber,
    string? Color,
    int ModelId
);
