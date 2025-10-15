using BikeRental.Domain.DataSeed;
using BikeRental.Domain.Enum;

namespace BikeRental.Tests;

/// <summary>
///     Contains a collection of analytical unit tests based on <see cref="DataSeed" /> data.
/// </summary>
public class BikeRentalTests(DataSeed seed) : IClassFixture<DataSeed>
{
    /// <summary>
    ///     Verifies that all bikes of type BikeType.Sport are correctly selected.
    /// </summary>
    [Fact(DisplayName = "Sport Bikes - Verify Model List")]
    public void InformationAboutSportBikes()
    {
        var sportBikes = seed.Bikes
            .Where(b => b.Model.BikeType == BikeType.Sport)
            .Select(b => new
            {
                b.Id,
                b.SerialNumber,
                b.Color,
                ModelId = b.Model.Id,
                Type = b.Model.BikeType
            })
            .ToList();

        Assert.Single(sportBikes);
        Assert.Equal(2, sportBikes[0].ModelId);
        Assert.Equal(BikeType.Sport, sportBikes[0].Type);
        Assert.Equal("2024R01015", sportBikes[0].SerialNumber);
    }

    /// <summary>
    ///     Calculates total rental revenue per model and validates the top 5 models.
    /// </summary>
    [Fact(DisplayName = "Top 5 Models by Revenue")]
    public void TopFiveModelsByRevenue()
    {
        var revenueByModel = seed.Rents
            .GroupBy(r => r.Bike.Model.Id)
            .Select(g => new
            {
                ModelId = g.Key,
                Revenue = g.Sum(r => r.Duration * r.Bike.Model.PricePerHour)
            })
            .OrderByDescending(x => x.Revenue)
            .ThenBy(x => x.ModelId)
            .Take(5)
            .ToList();

        Assert.Equal(new[] { 8, 2, 4, 9, 1 }, revenueByModel.Select(x => x.ModelId).ToArray());

        var expectedRevenue = new Dictionary<int, decimal>
        {
            [8] = 81.90m,
            [2] = 65.60m,
            [4] = 54.40m,
            [9] = 42.00m,
            [1] = 37.50m
        };

        foreach (var row in revenueByModel)
        {
            Assert.Equal(expectedRevenue[row.ModelId], Math.Round(row.Revenue, 2));
        }
    }

    /// <summary>
    ///     Calculates the total rental duration for each model and checks the top 5 models.
    /// </summary>
    [Fact(DisplayName = "Top 5 Models by Total Rental Duration")]
    public void TopFiveModelsByTotalDuration()
    {
        var durationByModel = seed.Rents
            .GroupBy(r => r.Bike.Model.Id)
            .Select(g => new
            {
                ModelId = g.Key,
                TotalHours = g.Sum(r => r.Duration)
            })
            .OrderByDescending(x => x.TotalHours)
            .ThenBy(x => x.ModelId)
            .Take(5)
            .ToList();

        Assert.Equal(new[] { 8, 2, 4, 5, 10 }, durationByModel.Select(x => x.ModelId).ToArray());
    }

    /// <summary>
    ///     Validates minimum, maximum, and average rental duration across all rental records.
    /// </summary>
    [Fact(DisplayName = "Min / Max / Avg Rental Duration")]
    public void MinMaxAvgRentalDuration()
    {
        var durations = seed.Rents.Select(r => r.Duration).ToArray();

        var min = durations.Min();
        var max = durations.Max();
        var avg = durations.Average();

        Assert.Equal(1, min);
        Assert.Equal(5, max);
        Assert.Equal(2.95, Math.Round(avg, 2));
    }

    /// <summary>
    ///     Verifies the total rental time for each bike type.
    /// </summary>
    [Theory(DisplayName = "Total Rental Time per Bike Type")]
    [InlineData(BikeType.Mountain, 10)]
    [InlineData(BikeType.Sport, 8)]
    [InlineData(BikeType.City, 13)]
    [InlineData(BikeType.Track, 14)]
    [InlineData(BikeType.Mini, 5)]
    [InlineData(BikeType.Electric, 9)]
    public void TotalRentalTimeByType(BikeType type, int expectedHours)
    {
        var actualHours = seed.Rents
            .Where(rent => rent.Bike.Model.BikeType == type)
            .Sum(rent => rent.Duration);

        Assert.Equal(expectedHours, actualHours);
    }

    /// <summary>
    ///     Determines the clients who rented bikes the most times,
    /// </summary>
    [Fact(DisplayName = "Clients with the Highest Number of Rentals")]
    public void TopClientsByRentalCount()
    {
        var counts = seed.Rents
            .GroupBy(r => r.Renter.Id)
            .Select(g => new
            {
                RenterId = g.Key,
                Count = g.Count()
            })
            .ToList();

        var maxCount = counts.Max(x => x.Count);
        var leaders = counts.Where(x => x.Count == maxCount).ToList();

        Assert.Equal(2, maxCount);
        Assert.Equal(10, leaders.Count);
        Assert.All(leaders, l => Assert.Equal(2, l.Count));
    }
}