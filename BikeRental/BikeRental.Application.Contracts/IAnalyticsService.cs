using BikeRental.Application.Contracts.Bike;
using BikeRental.Application.Contracts.Renter;

namespace BikeRental.Application.Contracts;

/// <summary>
/// A service for performing BikeRental analytical queries (covers scenarios from BikeRental.Tests).
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Gets a list of all Sport bikes.
    /// </summary>
    /// <returns>List of DTO bikes</returns>
    public Task<IList<BikeDto>> GetAllSportBikesAsync();

    /// <summary>
    /// Gets the top 5 models by total revenue.
    /// </summary>
    /// <returns>A list of pairs (modelId, Revenue) sorted in descending order of revenue.</returns>
    public Task<IList<KeyValuePair<int, decimal>>> GetTopFiveModelsByRevenueAsync();

    /// <summary>
    /// Gets the top 5 models by total rental time (hours).
    /// </summary>
    /// <returns>A list of pairs (modelId, TotalHours) sorted in descending order of hours.</returns>
    public Task<IList<KeyValuePair<int, int>>> GetTopFiveModelsByTotalDurationAsync();

    /// <summary>
    /// Returns the minimum, maximum, and average rental duration.
    /// </summary>
    /// <returns>Tuple (Min, Max, Avg)</returns>
    public Task<(int Min, int Max, double Avg)> GetMinMaxAvgRentDurationAsync();

    /// <summary>
    /// Gets the total rental time (in hours) for the specified bike type.
    /// </summary>
    /// <param name="type">Bike type</param>
    public Task<int> GetTotalRentalTimeByTypeAsync(int type);

    /// <summary>
    /// Gets clients with the highest number of rents.
    /// </summary>
    /// <returns>A list of pairs (RenterDto, Count) with the number of rents; all the leaders in case of a tie.</returns>
    public Task<IList<KeyValuePair<RenterDto, int>>> GetTopClientsByRentalCountAsync();
}