namespace BikeRental.Application.Contracts.Renter;

/// <summary>
/// DTO for creating or updating a renter.
/// </summary>
/// <param name="LastName">Renter's last name.</param>
/// <param name="Name">Renter's first name.</param>
/// <param name="MiddleName">Renter's middle name (optional).</param>
/// <param name="PhoneNumber">Contact phone number.</param>
public record RenterCreateUpdateDto(
    string LastName,
    string Name,
    string? MiddleName,
    string PhoneNumber
);
