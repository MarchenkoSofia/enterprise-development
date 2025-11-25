namespace BikeRental.Application.Contracts.Rent;
public record class RentCreateUpdateDto(
    DateTime StartTime,
    int Duration,
    int BikeId,
    int RenterId
);