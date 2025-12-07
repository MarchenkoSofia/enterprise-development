using BikeRental.Application.Contracts;
using BikeRental.Application.Contracts.Bike;
using BikeRental.Application.Contracts.Renter;
using Microsoft.AspNetCore.Mvc;

namespace BikeRental.Api.Controllers;

/// <summary>
/// [translate:Контроллер для выполнения аналитических запросов BikeRental.]
/// [translate:Покрывает сценарии из BikeRental.Tests: спорт-велосипеды, топ-модели по выручке и длительности аренды,
/// статистика длительности аренды, суммарное время по типу и топ-клиенты.]
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AnalyticsController : ControllerBase
{
    private readonly IAnalyticsService _analyticsService;
    private readonly ILogger<AnalyticsController> _logger;

    public AnalyticsController(
        IAnalyticsService analyticsService,
        ILogger<AnalyticsController> logger)
    {
        _analyticsService = analyticsService;
        _logger = logger;
    }

    /// <summary>
    /// [translate:Получает список всех велосипедов типа Sport.]
    /// </summary>
    /// <returns>[translate:Список DTO велосипедов типа Sport.]</returns>
    /// <response code="200">[translate:Список успешно получен.]</response>
    /// <response code="500">[translate:Внутренняя ошибка сервера.]</response>
    [HttpGet("bikes/sport")]
    public async Task<ActionResult<IList<BikeDto>>> GetAllSportBikesAsync()
    {
        _logger.LogInformation("Getting all sport bikes");

        try
        {
            var bikes = await _analyticsService.GetAllSportBikesAsync();
            return Ok(bikes);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all sport bikes");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// [translate:Получает топ-5 моделей по суммарной выручке.]
    /// </summary>
    /// <returns>[translate:Список пар (ModelId, Revenue), отсортированных по убыванию выручки.]</returns>
    /// <response code="200">[translate:Топ-5 моделей по выручке успешно получен.]</response>
    /// <response code="500">[translate:Внутренняя ошибка сервера.]</response>
    [HttpGet("models/top-revenue")]
    public async Task<ActionResult<IList<KeyValuePair<int, decimal>>>> GetTopFiveModelsByRevenueAsync()
    {
        _logger.LogInformation("Getting top 5 models by revenue");

        try
        {
            var result = await _analyticsService.GetTopFiveModelsByRevenueAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting top models by revenue");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// [translate:Получает топ-5 моделей по суммарному времени аренды (часы).]
    /// </summary>
    /// <returns>[translate:Список пар (ModelId, TotalHours), отсортированных по убыванию часов.]</returns>
    /// <response code="200">[translate:Топ-5 моделей по длительности аренды успешно получен.]</response>
    /// <response code="500">[translate:Внутренняя ошибка сервера.]</response>
    [HttpGet("models/top-duration")]
    public async Task<ActionResult<IList<KeyValuePair<int, int>>>> GetTopFiveModelsByTotalDurationAsync()
    {
        _logger.LogInformation("Getting top 5 models by total rent duration");

        try
        {
            var result = await _analyticsService.GetTopFiveModelsByTotalDurationAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting top models by duration");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// [translate:Возвращает минимальную, максимальную и среднюю длительность аренды.]
    /// </summary>
    /// <returns>[translate:Объект с полями Min, Max, Avg.]</returns>
    /// <response code="200">[translate:Статистика по длительности аренды успешно получена.]</response>
    /// <response code="500">[translate:Внутренняя ошибка сервера.]</response>
    [HttpGet("rents/min-max-avg")]
    public async Task<ActionResult<object>> GetMinMaxAvgRentDurationAsync()
    {
        _logger.LogInformation("Getting min, max and average rent duration");

        try
        {
            var (min, max, avg) = await _analyticsService.GetMinMaxAvgRentDurationAsync();
            return Ok(new { Min = min, Max = max, Avg = avg });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting min/max/avg rent duration");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// [translate:Получает суммарное время аренды (в часах) для указанного типа велосипеда.]
    /// </summary>
    /// <param name="type">[translate:Тип велосипеда (int по BikeType).]</param>
    /// <returns>[translate:Суммарное время аренды в часах.]</returns>
    /// <response code="200">[translate:Суммарное время аренды успешно получено.]</response>
    /// <response code="500">[translate:Внутренняя ошибка сервера.]</response>
    [HttpGet("rents/total-by-type")]
    public async Task<ActionResult<int>> GetTotalRentalTimeByTypeAsync([FromQuery] int type)
    {
        _logger.LogInformation("Getting total rental time for bike type {Type}", type);

        try
        {
            var total = await _analyticsService.GetTotalRentalTimeByTypeAsync(type);
            return Ok(total);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting total rental time by type");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// [translate:Получает клиентов с наибольшим числом аренд (все лидеры в случае ничьей).]
    /// </summary>
    /// <returns>[translate:Список пар (RenterDto, Count).]</returns>
    /// <response code="200">[translate:Топ-клиенты успешно получены.]</response>
    /// <response code="500">[translate:Внутренняя ошибка сервера.]</response>
    [HttpGet("renters/top-by-rents")]
    public async Task<ActionResult<IList<KeyValuePair<RenterDto, int>>>> GetTopClientsByRentalCountAsync()
    {
        _logger.LogInformation("Getting top clients by rental count");

        try
        {
            var result = await _analyticsService.GetTopClientsByRentalCountAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting top clients by rental count");
            return StatusCode(500, "Internal server error");
        }
    }
}
