using AutoMapper;
using BikeRental.Domain.Models;
using BikeRental.Infrastructure.EfCore;
using Bikes.Contracts.Grpc;
using Grpc.Core;
using Grpc.Net.Client;

namespace BikeRental.Api.GrpcServices;

/// <summary>
/// Background gRPC client that consumes generated bike models and saves them to the database.
/// </summary>
public class ModelConsumerService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<ModelConsumerService> _logger;
    private readonly IMapper _mapper;
    private readonly string _generatorUrl;
    private readonly int _batchSize;

    /// <summary>
    /// Initializes a new instance of the <see cref="ModelConsumerService"/> class.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when GeneratorGrpcUrl is not configured.</exception>
    public ModelConsumerService(
        IServiceScopeFactory scopeFactory,
        ILogger<ModelConsumerService> logger,
        IConfiguration config, 
        IMapper mapper)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
        _mapper = mapper;

        _generatorUrl = config["GeneratorGrpcUrl"]
            ?? throw new InvalidOperationException("GeneratorGrpcUrl not configured");

        _batchSize = config.GetValue<int?>("ModelBatchSize") ?? 10;
    }

    /// <summary>
    /// Connects to the gRPC stream and processes incoming bike models in a loop.
    /// </summary>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation(
            "Connecting to Bike Model Generator gRPC service at {Url} with BatchSize: {BatchSize}",
            _generatorUrl, _batchSize);

        using var channel = GrpcChannel.ForAddress(_generatorUrl);
        var client = new ModelGrpcService.ModelGrpcServiceClient(channel);

        try
        {
            using var call = client.AddModelsStream(cancellationToken: stoppingToken);

            var batch = new List<AddModelRequest>();

            await foreach (var model in call.ResponseStream.ReadAllAsync(stoppingToken))
            {
                batch.Add(model);

                _logger.LogInformation(
                    "Received model: Type={Type}, Year={Year}, Price={Price}",
                    model.BikeType, model.ModelYear, model.PricePerHour);

                if (batch.Count >= _batchSize)
                {
                    await SaveBatchAsync(batch, stoppingToken);
                    batch.Clear();
                }
            }

            if (batch.Count > 0)
            {
                await SaveBatchAsync(batch, stoppingToken);
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("ModelConsumerService is stopping due to cancellation token.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while consuming models via gRPC.");
        }
    }

    /// <summary>
    /// Persists a batch of generated bike models to the database within a new scope.
    /// </summary>
    private async Task SaveBatchAsync(
        IEnumerable<AddModelRequest> batch,
        CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BikeRentalDbContext>();

        foreach (var modelData in batch)
        {
            await SaveModelAsync(modelData, dbContext, cancellationToken);
        }

        _logger.LogInformation("Batch of {Count} models saved successfully.", batch.Count());
    }

    /// <summary>
    /// Maps a single gRPC model request into a domain entity via AutoMapper and adds it to the context.
    /// </summary>
    private async Task SaveModelAsync(
        AddModelRequest modelData,
        BikeRentalDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var model = _mapper.Map<Model>(modelData);

        dbContext.Models.Add(model);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
