namespace BikeRental.Application.Contracts.Rent;
/// <summary>
/// Service for working with rental records.
/// </summary>
public interface IRentService : IApplicationService<RentDto, RentCreateUpdateDto, int>
{
    /// <summary>
    /// Gets all rentals for the specified bike.
    /// </summary>
    public Task<IList<RentDto>> GetRentsByBikeAsync(int bikeId);

    /// <summary>
    /// Gets all the rentals for the specified renter.
    /// </summary>
    public Task<IList<RentDto>> GetRentsByRenterAsync(int renterId);

}