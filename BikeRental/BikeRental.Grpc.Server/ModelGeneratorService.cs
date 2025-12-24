using Bikes.Contracts.Grpc;
using Bogus;
using Grpc.Core;

namespace BikeRental.Grpc.Server;

/// <summary>
/// gRPC service that streams random bike models to a client and receives save status callbacks.
/// </summary>
public class ModelGeneratorService(
    ILogger<ModelGeneratorService> logger,
    IConfiguration config) : ModelGrpcService.ModelGrpcServiceBase
{
    private readonly Faker _faker = new("en");
    private readonly int _delaySeconds = config.GetValue<int?>("GeneratorDelaySeconds") ?? 1;

    /// <summary>
    /// Streams generated bike models to the client and receives save status callbacks.
    /// </summary>
    public override async Task AddModelsStream(
        IAsyncStreamReader<AddModelResponse> requestStream,
        IServerStreamWriter<AddModelRequest> responseStream,
        ServerCallContext context)
    {
        logger.LogInformation("Client connected to the bike model generator.");

        var generatorTask = Task.Run(async () =>
        {
            var generatedCount = 0;

            while (!context.CancellationToken.IsCancellationRequested)
            {
                var model = GenerateRandomModel();
                generatedCount++;

                logger.LogInformation(
                    "Generated model #{Count}: Type={Type}, Year={Year}, PricePerHour={Price}",
                    generatedCount, model.BikeType, model.ModelYear, model.PricePerHour);

                await responseStream.WriteAsync(model, context.CancellationToken);
                await Task.Delay(TimeSpan.FromSeconds(_delaySeconds), context.CancellationToken);
            }
        }, context.CancellationToken);

        var callbackTask = Task.Run(async () =>
        {
            await foreach (var callback in requestStream.ReadAllAsync(context.CancellationToken))
            {
                if (callback.Success)
                {
                    logger.LogInformation(
                        "Client confirmed model persistence (Id={Id}): {Message}",
                        callback.ModelId, callback.Message);
                }
                else
                {
                    logger.LogWarning(
                        "Client reported a persistence error: {Message}",
                        callback.Message);
                }
            }
        }, context.CancellationToken);

        try
        {
            await Task.WhenAll(generatorTask, callbackTask);
        }
        catch (OperationCanceledException)
        {
            logger.LogInformation("Client disconnected from the bike model generator.");
        }
    }

    /// <summary>
    /// Generates a single random bike model definition aligned with domain bike types.
    /// </summary>
    private AddModelRequest GenerateRandomModel()
    {
        var bikeType = _faker.PickRandom("mountain", "city", "electric", "track", "mini", "sport");
        var brakeTypes = new[] { "Disc", "Rim", "V-Brake" };

        return new AddModelRequest
        {
            WheelSize = _faker.PickRandom(new[] { 26d, 27d, 29d }),
            MaxPassengerWeight = _faker.Random.Double(90, 140),
            BikeWeight = _faker.Random.Double(10, 22),
            BrakeType = _faker.PickRandom(brakeTypes),
            ModelYear = _faker.Random.Int(2018, 2025),
            PricePerHour = Math.Round(_faker.Random.Double(3, 15), 2),
            BikeType = bikeType
        };
    }
}
