namespace BikeRental.Application.Contracts.Rent;

/// <summary>
/// Data transfer object representing a bike rental record.
/// </summary>
/// <param name="Id">Unique identifier of the rent.</param>
/// <param name="StartTime">Date and time when the rental started.</param>
/// <param name="Duration">Duration of the rental in hours.</param>
/// <param name="BikeId">Identifier of the rented bike.</param>
/// <param name="RenterId">Identifier of the renter.</param>
public record class RentDto(
    int Id,
    DateTime StartTime,
    int Duration,
    int BikeId,
    int RenterId
);
