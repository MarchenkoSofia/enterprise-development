using BikeRental.Grpc.Server;
using Bikes.Contracts.Grpc;
using Bogus;

namespace BikeRental.Grpc.Server.Services;

/// <summary>
/// A factory for generating random bike model data using the Bogus library.
/// </summary>
public sealed class RandomBikeModelFactory : IBikeModelFactory
{
    private readonly Faker _faker = new("en");

    /// <summary>
    /// Creates a new instance of a bike model request with randomized properties.
    /// </summary>
    public AddModelRequest Create()
    {
        var brakeTypes = new[] { BrakeType.Disc, BrakeType.Rim, BrakeType.VBrake };

        return new()
        {
            WheelSize = _faker.PickRandom(new[] { 26d, 27d, 29d }),
            MaxPassengerWeight = _faker.Random.Double(90, 140),
            BikeWeight = _faker.Random.Double(10, 22),
            BrakeType = _faker.PickRandom(brakeTypes),
            ModelYear = _faker.Random.Int(2018, 2025),
            PricePerHour = Math.Round(_faker.Random.Double(3, 15), 2),
            BikeType = _faker.PickRandom(new[] { GrpcBikeType.Mountain, GrpcBikeType.City, GrpcBikeType.Electric,
                GrpcBikeType.Track, GrpcBikeType.Mini, GrpcBikeType.Sport})
        };
    }
}
