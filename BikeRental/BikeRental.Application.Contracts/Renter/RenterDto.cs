namespace BikeRental.Application.Contracts.Renter;

/// <summary>
/// Data transfer object representing a renter.
/// </summary>
/// <param name="Id">Unique identifier.</param>
/// <param name="LastName">Renter's last name.</param>
/// <param name="Name">Renter's first name.</param>
/// <param name="MiddleName">Renter's middle name (optional).</param>
/// <param name="PhoneNumber">Contact phone number.</param>
public record RenterDto(
    int Id,
    string LastName,
    string Name,
    string? MiddleName,
    string PhoneNumber
);
