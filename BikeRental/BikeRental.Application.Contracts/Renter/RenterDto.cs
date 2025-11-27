namespace BikeRental.Application.Contracts.Renter;
public record RenterDto(
    int Id,
    string LastName,
    string Name,
    string? MiddleName,
    string PhoneNumber
);