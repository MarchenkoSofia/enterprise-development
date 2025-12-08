using BikeRental.Application.Contracts.Bike;
using BikeRental.Application.Contracts.Rent;
using Microsoft.AspNetCore.Mvc;

namespace BikeRental.Api.Controllers;

/// <summary>
/// Controller for managing bikes and their related operations.
/// </summary>
/// <param name="bikeService">Application service for bikes.</param>
/// <param name="rentService">Application service for rentals.</param>
/// <param name="logger">Logger instance.</param>
[ApiController]
[Route("api/[controller]")]
public class BikesController(
    IBikeService bikeService,
    IRentService rentService,
    ILogger<BikesController> logger)
    : CrudControllerBase<BikeDto, BikeCreateUpdateDto, int>(bikeService, logger)
{
    /// <summary>
    /// Retrieves all rentals associated with a specific bike.
    /// </summary>
    /// <param name="id">The unique identifier of the bike.</param>
    /// <returns>A list of rentals for the specified bike.</returns>
    /// <response code="200">Returns the list of rentals.</response>
    /// <response code="204">If the bike has no rentals found.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet("{id}/rents")]
    [ProducesResponseType(typeof(IList<RentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IList<RentDto>>> GetRents(int id)
    {
        logger.LogInformation("{Method} called on {Controller} with id={Id}",
            nameof(GetRents), nameof(BikesController), id);

        try
        {
            var rents = await rentService.GetRentsByBikeAsync(id);

            return rents != null && rents.Any()
                ? Ok(rents)
                : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception in {Method} of {Controller}",
                nameof(GetRents), nameof(BikesController));

            return StatusCode(500, ex.Message);
        }
    }
}
