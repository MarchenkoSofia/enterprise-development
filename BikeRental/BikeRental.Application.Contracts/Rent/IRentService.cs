namespace BikeRental.Application.Contracts.Rent;
/// <summary>
/// Сервис для работы с записями аренды.
/// </summary>
public interface IRentService : IApplicationService<RentDto, RentCreateUpdateDto, int>
{
    /// <summary>
    /// Получает все аренды для указанного велосипеда.
    /// </summary>
    public Task<IList<RentDto>> GetRentsByBikeAsync(int bikeId);

    /// <summary>
    /// Получает все аренды для указанного арендатора.
    /// </summary>
    public Task<IList<RentDto>> GetRentsByRenterAsync(int renterId);

    /// <summary>
    /// Получает все аренды, сгруппированные по идентификатору модели велосипеда.
    /// Удобно для вычисления выручки/времени по модели.
    /// </summary>
    public Task<IList<KeyValuePair<int, IList<RentDto>>>> GetRentsGroupedByModelAsync();
}