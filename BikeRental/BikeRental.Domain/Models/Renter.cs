namespace BikeRental.Domain.Models;

/// <summary>
/// A class describing renter and information about them.
/// </summary>
public class Renter
{
    /// <summary>
    /// Unique identifier of the renter.
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Last name of the renter.
    /// </summary>
    public required string LastName { get; set; }

    /// <summary>
    /// First name of the renter.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Middle name of the renter.
    /// </summary>
    public string? MiddleName { get; set; }

    /// <summary>
    /// Phone number of the renter.
    /// </summary>
    public required string PhoneNumber { get; set; }
}