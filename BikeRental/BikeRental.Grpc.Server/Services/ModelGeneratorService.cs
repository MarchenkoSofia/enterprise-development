using Bikes.Contracts.Grpc;
using Grpc.Core;

namespace BikeRental.Grpc.Server.Services;

/// <summary>
/// gRPC service responsible for continuously generating and streaming bike models to connected clients.
/// </summary>
public class ModelGeneratorService(
    ILogger<ModelGeneratorService> logger,
    IConfiguration config,
    IBikeModelFactory modelFactory) : ModelGrpcService.ModelGrpcServiceBase
{
    private readonly int delaySeconds = config.GetValue<int?>("GeneratorDelaySeconds") ?? 1;

    /// <summary>
    /// Handles the bi-directional stream by starting concurrent tasks for generating models and processing client callbacks.
    /// </summary>
    public override async Task AddModelsStream(
        IAsyncStreamReader<AddModelResponse> requestStream,
        IServerStreamWriter<AddModelRequest> responseStream,
        ServerCallContext context)
    {
        logger.LogInformation("Client connected. Starting model generation session.");

        var readingTask = ProcessCallbacksAsync(requestStream, context.CancellationToken);

        await GenerateModelsAsync(responseStream, context.CancellationToken);
    }

    /// <summary>
    /// continuously creates new bike models using the factory and writes them to the response stream until cancellation.
    /// </summary>
    private async Task GenerateModelsAsync(
        IServerStreamWriter<AddModelRequest> responseStream,
        CancellationToken cancellationToken)
    {
        try
        {
            var count = 0;
            while (!cancellationToken.IsCancellationRequested)
            {
                var model = modelFactory.Create();
                count++;

                logger.LogInformation("Generated Model #{Count}: {Type}, {Year}, ${Price:F2}",
                    count, model.BikeType, model.ModelYear, model.PricePerHour);

                await responseStream.WriteAsync(model, cancellationToken);
                await Task.Delay(TimeSpan.FromSeconds(delaySeconds), cancellationToken);
            }
        }
        catch (OperationCanceledException)
        {
            logger.LogInformation("Generation stopped: Client disconnected.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error during model generation.");
        }
    }

    /// <summary>
    /// Reads and logs confirmation messages or errors sent back from the client.
    /// </summary>
    private async Task ProcessCallbacksAsync(
        IAsyncStreamReader<AddModelResponse> requestStream,
        CancellationToken cancellationToken)
    {
        await Task.Yield();

        try
        {
            await foreach (var callback in requestStream.ReadAllAsync(cancellationToken))
            {
                if (callback.Success)
                {
                    logger.LogInformation("Client confirmed save: Model {Id}, {Message}",
                        callback.ModelId, callback.Message);
                }
                else
                {
                    logger.LogWarning("Client failed to save: {Message}", callback.Message);
                }
            }
        }
        catch (Exception ex) when (ex is IOException || ex is RpcException)
        {
            logger.LogWarning("Callback processing stopped (Client disconnected or silent).");
        }
        catch (OperationCanceledException){}
        catch (Exception ex)
        {
            logger.LogError(ex, "Error processing client callbacks.");
        }
    }
}
