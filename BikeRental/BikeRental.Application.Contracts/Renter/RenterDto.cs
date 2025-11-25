namespace BikeRental.Application.Contracts.Rental;
public record RenterDto(
    int Id,
    string LastName,
    string Name,
    string? MiddleName,
    string PhoneNumber
);