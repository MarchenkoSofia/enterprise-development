namespace BikeRental.Application.Contracts.Renter;
public record RenterCreateUpdateDto(
    string LastName,
    string Name,
    string? MiddleName,
    string PhoneNumber
);
