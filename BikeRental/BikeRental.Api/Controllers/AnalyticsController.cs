using BikeRental.Application.Contracts;
using BikeRental.Application.Contracts.Bike;
using BikeRental.Application.Contracts.Renter;
using Microsoft.AspNetCore.Mvc;

namespace BikeRental.Api.Controllers;

/// <summary>
/// Controller for business analytics and statistical reports.
/// Covers scenarios such as sport bikes, revenue analysis, rent duration stats, and top clients.
/// </summary>
/// <param name="analyticsService">Service for analytics logic.</param>
/// <param name="logger">Logger instance.</param>
[ApiController]
[Route("api/[controller]")]
public class AnalyticsController(
    IAnalyticsService analyticsService,
    ILogger<AnalyticsController> logger) : ControllerBase
{
    /// <summary>
    /// Retrieves a list of all bicycles classified as "Sport" type.
    /// </summary>
    /// <returns>List of sport bike DTOs.</returns>
    /// <response code="200">Returns the list of sport bikes.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet("bikes/sport")]
    [ProducesResponseType(typeof(IList<BikeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IList<BikeDto>>> GetAllSportBikesAsync()
    {
        logger.LogInformation("Getting all sport bikes");

        try
        {
            var bikes = await analyticsService.GetAllSportBikesAsync();
            return Ok(bikes);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting all sport bikes");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Retrieves the top 5 bike models by total revenue.
    /// </summary>
    /// <returns>List of key-value pairs (ModelId, Revenue), sorted by revenue descending.</returns>
    /// <response code="200">Returns top models by revenue.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet("models/top-revenue")]
    [ProducesResponseType(typeof(IList<KeyValuePair<int, decimal>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IList<KeyValuePair<int, decimal>>>> GetTopFiveModelsByRevenueAsync()
    {
        logger.LogInformation("Getting top 5 models by revenue");

        try
        {
            var result = await analyticsService.GetTopFiveModelsByRevenueAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting top models by revenue");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Retrieves the top 5 bike models by total rental duration (in hours).
    /// </summary>
    /// <returns>List of key-value pairs (ModelId, TotalHours), sorted by duration descending.</returns>
    /// <response code="200">Returns top models by duration.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet("models/top-duration")]
    [ProducesResponseType(typeof(IList<KeyValuePair<int, int>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IList<KeyValuePair<int, int>>>> GetTopFiveModelsByTotalDurationAsync()
    {
        logger.LogInformation("Getting top 5 models by total rent duration");

        try
        {
            var result = await analyticsService.GetTopFiveModelsByTotalDurationAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting top models by duration");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Calculates minimum, maximum, and average rental durations.
    /// </summary>
    /// <returns>An object containing Min, Max, and Avg duration values.</returns>
    /// <response code="200">Returns rental duration statistics.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet("rents/min-max-avg")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<object>> GetMinMaxAvgRentDurationAsync()
    {
        logger.LogInformation("Getting min, max and average rent duration");

        try
        {
            var (min, max, avg) = await analyticsService.GetMinMaxAvgRentDurationAsync();
            return Ok(new { Min = min, Max = max, Avg = avg });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting min/max/avg rent duration");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Calculates the total rental time (in hours) for a specific bike type.
    /// </summary>
    /// <param name="type">The integer representation of the bike type (1-5).</param>
    /// <returns>Total rental hours for the specified type.</returns>
    /// <response code="200">Returns the total rental time.</response>
    /// <response code="400">If the bike type is invalid (not between 0 and 5).</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet("rents/total-by-type")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<int>> GetTotalRentalTimeByTypeAsync([FromQuery] int type)
    {
        logger.LogInformation("Getting total rental time for bike type {Type}", type);

        if (type < 0 || type > 5)
        {
            logger.LogWarning("Invalid bike type: {Type}", type);
            return BadRequest("Bike type must be between 0 and 5.");
        }

        try
        {
            var total = await analyticsService.GetTotalRentalTimeByTypeAsync(type);
            return Ok(total);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting total rental time by type");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Retrieves clients (renters) with the highest number of completed rentals.
    /// </summary>
    /// <returns>List of key-value pairs (RenterDto, Count). Returns all leaders in case of a tie.</returns>
    /// <response code="200">Returns the top clients.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet("renters/top-by-rents")]
    [ProducesResponseType(typeof(IList<KeyValuePair<RenterDto, int>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IList<KeyValuePair<RenterDto, int>>>> GetTopClientsByRentalCountAsync()
    {
        logger.LogInformation("Getting top clients by rental count");

        try
        {
            var result = await analyticsService.GetTopClientsByRentalCountAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting top clients by rental count");
            return StatusCode(500, "Internal server error");
        }
    }
}
