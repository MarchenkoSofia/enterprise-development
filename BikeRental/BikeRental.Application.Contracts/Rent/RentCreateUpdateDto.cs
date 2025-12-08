namespace BikeRental.Application.Contracts.Rent;

/// <summary>
/// DTO for creating or updating a rent record.
/// </summary>
/// <param name="StartTime">Date and time when the rent starts.</param>
/// <param name="Duration">Duration of the rent in hours.</param>
/// <param name="BikeId">Identifier of the bike to rent.</param>
/// <param name="RenterId">Identifier of the renter.</param>
public record class RentCreateUpdateDto(
    DateTime StartTime,
    int Duration,
    int BikeId,
    int RenterId
);
