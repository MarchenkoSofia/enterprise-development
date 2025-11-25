namespace BikeRental.Application.Contracts.Rent;
public record class RentDto(
    int Id,
    DateTime StartTime,
    int Duration,
    int BikeId,
    int RenterId
);