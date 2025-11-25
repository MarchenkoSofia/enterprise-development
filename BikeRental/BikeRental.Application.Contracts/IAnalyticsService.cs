using BikeRental.Application.Contracts.Bike;
using BikeRental.Application.Contracts.Model;
using BikeRental.Application.Contracts.Rent;
using BikeRental.Application.Contracts.Rental;
using BikeRental.Domain.Enum;

namespace BikeRental.Application.Contracts;

    /// <summary>
    /// Служба для выполнения аналитических запросов BikeRental (покрывает сценарии из BikeRental.Tests).
    /// </summary>
    public interface IAnalyticsService
{
    /// <summary>
    /// Получает список всех велосипедов типа Sport.
    /// </summary>
    /// <returns>Список DTO велосипедов</returns>
    public Task<IList<BikeDto>> GetAllSportBikesAsync();

    /// <summary>
    /// Получает топ-5 моделей по суммарной выручке.
    /// </summary>
    /// <returns>Список пар (ModelId, Revenue) отсортированных по убыванию выручки.</returns>
    public Task<IList<KeyValuePair<int, decimal>>> GetTopFiveModelsByRevenueAsync();

    /// <summary>
    /// Получает топ-5 моделей по суммарному времени аренды (часы).
    /// </summary>
    /// <returns>Список пар (ModelId, TotalHours) отсортированных по убыванию часов.</returns>
    public Task<IList<KeyValuePair<int, int>>> GetTopFiveModelsByTotalDurationAsync();

    /// <summary>
    /// Возвращает минимальную, максимальную и среднюю длительность аренды.
    /// </summary>
    /// <returns>Кортеж (Min, Max, Avg)</returns>
    public Task<(int Min, int Max, double Avg)> GetMinMaxAvgRentDurationAsync();

    /// <summary>
    /// Получает суммарное время аренды (в часах) для указанного типа велосипеда.
    /// </summary>
    /// <param name="type">Тип велосипеда</param>
    public Task<int> GetTotalRentalTimeByTypeAsync(BikeType type);

    /// <summary>
    /// Получает клиентов с наибольшим числом аренд.
    /// </summary>
    /// <returns>Список пар (RenterDto, Count) с количеством аренд; все лидеры в случае ничьей.</returns>
    public Task<IList<KeyValuePair<RenterDto, int>>> GetTopClientsByRentalCountAsync();
}

