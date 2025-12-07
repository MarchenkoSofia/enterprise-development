

namespace BikeRental.Application.Contracts.Bike;
/// <summary>
/// Сервис для работы с велосипедами.
/// </summary>
public interface IBikeService : IApplicationService<BikeDto, BikeCreateUpdateDto, int>
{
    /// <summary>
    /// Получает список велосипедов указанной модели.
    /// </summary>
    public Task<IList<BikeDto>> GetBikesByModelAsync(int modelId);

}