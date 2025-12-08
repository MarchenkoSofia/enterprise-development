namespace BikeRental.Application.Contracts.Bike;
/// <summary>
/// Service for working with bikes.
/// </summary>
public interface IBikeService : IApplicationService<BikeDto, BikeCreateUpdateDto, int>
{
    /// <summary>
    /// Gets a list of bikes of the specified model.
    /// </summary>
    public Task<IList<BikeDto>> GetBikesByModelAsync(int modelId);

}