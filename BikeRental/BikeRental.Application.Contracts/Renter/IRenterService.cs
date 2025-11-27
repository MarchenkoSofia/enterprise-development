
namespace BikeRental.Application.Contracts.Renter;
/// <summary>
/// Сервис для работы с арендаторами (клиентами).
/// </summary>
public interface IRenterService : IApplicationService<RenterDto, RenterCreateUpdateDto, int>
{
    /// <summary>
    /// Получает все аренды указанного клиента.
    /// </summary>
    public Task<IList<Rent.RentDto>> GetRenterRentsAsync(int renterId);
}