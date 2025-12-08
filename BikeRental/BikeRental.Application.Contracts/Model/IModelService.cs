using BikeRental.Application.Contracts.Bike;

namespace BikeRental.Application.Contracts.Model;

/// <summary>
/// Service for working with bike models.
/// </summary>
public interface IModelService : IApplicationService<ModelDto, ModelCreateUpdateDto, int>
{
    /// <summary>
    /// Gets a list of all bikes related to the model.
    /// </summary>
    public Task<IList<BikeDto>> GetBikesAsync(int modelId);
}