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
            .Join(
                seed.Models,
                b => b.ModelId,
                m => m.Id,
                (b, m) => new { Bike = b, Model = m }
            )
            .Where(x => x.Model.BikeType == BikeType.Sport)
            .Select(x => new
            {
                x.Bike.Id,
                x.Bike.SerialNumber,
                x.Bike.Color,
                ModelId = x.Model.Id,
                Type = x.Model.BikeType
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
        var revenueByModel =
            seed.Rents
                .Join(
                    seed.Bikes,
                    rent => rent.BikeId,
                    bike => bike.Id,
                    (rent, bike) => new { rent, bike }
                )
                .Join(
                    seed.Models,
                    x => x.bike.ModelId,
                    model => model.Id,
                    (x, model) => new { x.rent, model }
                )
                .GroupBy(x => x.model.Id)
                .Select(g => new
                {
                    ModelId = g.Key,
                    Revenue = g.Sum(x => x.rent.Duration * x.model.PricePerHour)
                })
                .OrderByDescending(x => x.Revenue)
                .ThenBy(x => x.ModelId)
                .Take(5)
                .ToList();

        Assert.Equal(
            new[] { 2, 1, 4, 5, 3 },
            revenueByModel.Select(x => x.ModelId).ToArray()
        );

        var expectedRevenue = new Dictionary<int, decimal>
        {
            [2] = 82.00m,
            [1] = 37.50m,
            [4] = 34.00m,
            [5] = 18.00m,
            [3] = 17.70m
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
        var durationByModel =
            seed.Rents
                .Join(
                    seed.Bikes,
                    rent => rent.BikeId,
                    bike => bike.Id,
                    (rent, bike) => new { rent, bike }
                )
                .GroupBy(x => x.bike.ModelId)
                .Select(g => new
                {
                    ModelId = g.Key,
                    TotalHours = g.Sum(x => x.rent.Duration)
                })
                .OrderByDescending(x => x.TotalHours)
                .ThenBy(x => x.ModelId)
                .Take(5)
                .ToList();

        Assert.Equal(
            new[] { 2, 1, 4, 5, 3 },
            durationByModel.Select(x => x.ModelId).ToArray()
        );
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
        Assert.Equal(2.7, Math.Round(avg, 2));
    }

    /// <summary>
    ///     Verifies the total rental time for each bike type.
    /// </summary>
    [Theory(DisplayName = "Total Rental Time per Bike Type")]
    [InlineData(BikeType.Mountain, 5)]
    [InlineData(BikeType.Sport, 10)]
    [InlineData(BikeType.City, 3)]
    [InlineData(BikeType.Track, 9)]
    [InlineData(BikeType.Mini, 0)]
    [InlineData(BikeType.Electric, 0)]
    public void TotalRentalTimeByType(BikeType type, int expectedHours)
    {
        var actualHours =
            (from rent in seed.Rents
                join bike in seed.Bikes on rent.BikeId equals bike.Id
                join model in seed.Models on bike.ModelId equals model.Id
                where model.BikeType == type
                select rent.Duration)
            .Sum();

        Assert.Equal(expectedHours, actualHours);
    }

    /// <summary>
    ///     Determines the clients who rented bikes the most times,
    /// </summary>
    [Fact(DisplayName = "Clients with the Highest Number of Rentals")]
    public void TopClientsByRentalCount()
    {
        var counts = seed.Rents
            .GroupBy(r => r.RenterId)
            .Select(g => new
            {
                RenterId = g.Key,
                Count = g.Count()
            })
            .ToList();

        var maxCount = counts.Max(x => x.Count);
        var leaders = counts.Where(x => x.Count == maxCount).ToList();

        Assert.Equal(1, maxCount);
        Assert.Equal(10, leaders.Count);
        Assert.All(leaders, l => Assert.Equal(1, l.Count));
    }
}