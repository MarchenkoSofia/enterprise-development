using BikeRental.Application.Contracts.Rent;
using BikeRental.Application.Contracts.Renter;
using Microsoft.AspNetCore.Mvc;

namespace BikeRental.Api.Controllers;

/// <summary>
/// Controller for managing renters (clients) and their specific operations.
/// </summary>
/// <param name="renterService">Application service for renters.</param>
/// <param name="rentService">Application service for rentals.</param>
/// <param name="logger">Logger instance.</param>
[ApiController]
[Route("api/[controller]")]
public class RentersController(
    IRenterService renterService,
    IRentService rentService,
    ILogger<RentersController> logger)
    : CrudControllerBase<RenterDto, RenterCreateUpdateDto, int>(renterService, logger)
{
    /// <summary>
    /// Retrieves all rentals associated with a specific renter.
    /// </summary>
    /// <param name="id">The unique identifier of the renter.</param>
    /// <returns>A list of rents for the specified renter, or NoContent if none found.</returns>
    /// <response code="200">Returns the list of rentals.</response>
    /// <response code="204">If the renter has no rentals found.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet("{id}/rents")]
    [ProducesResponseType(typeof(IList<RentDto>), StatusCodes.Status201Created)] 
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IList<RentDto>>> GetRents(int id)
    {
        logger.LogInformation("{Method} called on {Controller} with id={Id}",
            nameof(GetRents), nameof(RentersController), id);

        try
        {
            var res = await rentService.GetRentsByRenterAsync(id);

            return res != null && res.Any()
                ? Ok(res)
                : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception in {Method} of {Controller}",
                nameof(GetRents), nameof(RentersController));

            return StatusCode(500, ex.Message);
        }
    }
}
