namespace BikeRental.Application.Contracts.Rental;
public record RenterCreateUpdateDto(
    string LastName,
    string Name,
    string? MiddleName,
    string PhoneNumber
);
