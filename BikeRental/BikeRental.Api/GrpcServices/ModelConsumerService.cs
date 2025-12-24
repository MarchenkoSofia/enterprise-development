using BikeRental.Domain.Enum;
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
    private readonly IConfiguration _config;
    private readonly string _generatorUrl;
    private readonly int _batchSize;

    /// <summary>
    /// Initializes the consumer service and reads generator configuration settings.
    /// </summary>
    public ModelConsumerService(
        IServiceScopeFactory scopeFactory,
        ILogger<ModelConsumerService> logger,
        IConfiguration config)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
        _config = config;

        _generatorUrl = config["GeneratorGrpcUrl"]
            ?? throw new InvalidOperationException("GeneratorGrpcUrl not configured");

        _batchSize = config.GetValue<int?>("ModelBatchSize") ?? 10;
    }

    /// <summary>
    /// Opens a gRPC stream to the model generator and processes incoming models in batches.
    /// </summary>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation(
            "Подключение к gRPC‑генератору моделей: {Url}, размер батча: {BatchSize}",
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
                    "Получена модель: Тип={Type}, Год={Year}, Цена={Price}",
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

            await call.RequestStream.CompleteAsync();
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Остановка ModelConsumerService по токену отмены");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении моделей по gRPC");
        }
    }

    /// <summary>
    /// Persists a batch of generated bike models within a scoped database context.
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

        _logger.LogInformation("→ Сохранён батч из {Count} моделей", batch.Count());
    }

    /// <summary>
    /// Maps a single gRPC model request into a domain entity and saves it to the database.
    /// </summary>
    private static async Task SaveModelAsync(
    AddModelRequest modelData,
    BikeRentalDbContext dbContext,
    CancellationToken cancellationToken)
    {
        var bikeType = MapBikeType(modelData.BikeType);

        var model = new Model
        {
            Id = 0,
            WheelSize = modelData.WheelSize,
            MaxPassengerWeight = modelData.MaxPassengerWeight,
            BikeWeight = modelData.BikeWeight,
            BrakeType = modelData.BrakeType,
            ModelYear = modelData.ModelYear,
            PricePerHour = (decimal)modelData.PricePerHour,
            BikeType = bikeType
        };

        dbContext.Models.Add(model);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Converts a gRPC bike type string into the corresponding domain BikeType value.
    /// </summary>
    private static BikeType MapBikeType(string grpcType)
    {
        return grpcType.ToLowerInvariant() switch
        {
            "mountain" => BikeType.Mountain,
            "city" => BikeType.City,
            "electric" => BikeType.Electric,
            "track" => BikeType.Track,
            "mini" => BikeType.Mini,
            "sport" => BikeType.Sport,

            _ => BikeType.Sport 
        };
    }

}