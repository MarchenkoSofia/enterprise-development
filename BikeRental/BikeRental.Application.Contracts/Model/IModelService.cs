namespace BikeRental.Application.Contracts.Model;

/// <summary>
/// Сервис для работы с моделями велосипедов.
/// </summary>
public interface IModelService : IApplicationService<ModelDto, ModelCreateUpdateDto, int>
{
    /// <summary>
    /// Получает список всех велосипедов, относящихся к модели.
    /// </summary>
    public Task<IList<Bike.BikeDto>> GetBikesAsync(int modelId);
}