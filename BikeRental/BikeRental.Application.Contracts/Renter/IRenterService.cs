namespace BikeRental.Application.Contracts.Renter;
/// <summary>
/// Service for working with tenants (customers).
/// </summary>
public interface IRenterService : IApplicationService<RenterDto, RenterCreateUpdateDto, int>
{
    /// <summary>
    /// Gets all the rentals of the specified customer.
    /// </summary>
    public Task<IList<Rent.RentDto>> GetRenterRentsAsync(int renterId);
}