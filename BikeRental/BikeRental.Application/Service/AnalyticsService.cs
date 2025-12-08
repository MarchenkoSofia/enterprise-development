using AutoMapper;
using BikeRental.Application.Contracts;
using BikeRental.Application.Contracts.Bike;
using BikeRental.Application.Contracts.Renter;
using BikeRental.Domain;
using BikeRental.Domain.Enum;
using BikeRental.Domain.Models;

namespace BikeRental.Application.Service;

/// <summary>
/// Service responsible for business analytics and statistical queries.
/// </summary>
public class AnalyticsService(
    IRepository<Bike, int> bikeRepository,
    IRepository<Rent, int> rentRepository,
    IMapper mapper
) : IAnalyticsService
{
    /// <summary>
    /// Retrieves all bikes classified as "Sport" type.
    /// </summary>
    public async Task<IList<BikeDto>> GetAllSportBikesAsync()
    {
        var bikes = await bikeRepository.ReadAll();
        var sport = bikes.Where(b => b.Model.BikeType == BikeType.Sport).ToList();
        return mapper.Map<List<BikeDto>>(sport);
    }

    /// <summary>
    /// Retrieves the top 5 bike models generating the highest revenue.
    /// Revenue is calculated as duration * price per hour.
    /// </summary>
    public async Task<IList<KeyValuePair<int, decimal>>> GetTopFiveModelsByRevenueAsync()
    {
        var rents = await rentRepository.ReadAll();

        var result = rents
            .GroupBy(r => r.Bike.Model.Id)
            .Select(g => new
            {
                ModelId = g.Key,
                Revenue = g.Sum(r => r.Duration * r.Bike.Model.PricePerHour)
            })
            .OrderByDescending(x => x.Revenue)
            .ThenBy(x => x.ModelId)
            .Take(5)
            .Select(x => new KeyValuePair<int, decimal>(x.ModelId, Math.Round(x.Revenue, 2)))
            .ToList();

        return result;
    }

    /// <summary>
    /// Retrieves the top 5 bike models by total rental duration (in hours).
    /// </summary>
    public async Task<IList<KeyValuePair<int, int>>> GetTopFiveModelsByTotalDurationAsync()
    {
        var rents = await rentRepository.ReadAll();

        return rents
            .GroupBy(r => r.Bike.Model.Id)
            .Select(g => new KeyValuePair<int, int>(
                g.Key,
                g.Sum(r => r.Duration)))
            .OrderByDescending(x => x.Value)
            .ThenBy(x => x.Key)
            .Take(5)
            .ToList();
    }

    /// <summary>
    /// Calculates minimum, maximum, and average rental durations across all rents.
    /// </summary>
    public async Task<(int Min, int Max, double Avg)> GetMinMaxAvgRentDurationAsync()
    {
        var rents = await rentRepository.ReadAll();
        if (!rents.Any())
            return (0, 0, 0);

        var arr = rents.Select(r => r.Duration).ToArray();
        return (arr.Min(), arr.Max(), Math.Round(arr.Average(), 2));
    }

    /// <summary>
    /// Calculates the total rental time (in hours) for a specific bike type.
    /// </summary>
    public async Task<int> GetTotalRentalTimeByTypeAsync(int type)
    {
        var rents = await rentRepository.ReadAll();
        return rents
            .Where(r => (int)r.Bike.Model.BikeType == type)
            .Sum(r => r.Duration);
    }

    /// <summary>
    /// Retrieves the clients (renters) with the highest number of completed rentals.
    /// Returns all leaders in case of a tie.
    /// </summary>
    public async Task<IList<KeyValuePair<RenterDto, int>>> GetTopClientsByRentalCountAsync()
    {
        var rents = await rentRepository.ReadAll();
        if (!rents.Any())
            return new List<KeyValuePair<RenterDto, int>>();

        var grouped = rents
            .GroupBy(r => r.Renter)
            .Select(g => new { Renter = g.Key, Count = g.Count() })
            .ToList();

        var max = grouped.Max(g => g.Count);

        var leaders = grouped
            .Where(x => x.Count == max)
            .Select(x => new KeyValuePair<RenterDto, int>(
                mapper.Map<RenterDto>(x.Renter),
                x.Count))
            .ToList();

        return leaders;
    }
}
