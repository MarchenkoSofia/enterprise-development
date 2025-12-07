using BikeRental.Application.Contracts.Renter;
using BikeRental.Application.Contracts.Rent;
using Microsoft.AspNetCore.Mvc;

namespace BikeRental.Api.Controllers;

/// <summary>
/// Controller for renter (client) CRUD operations and renter-specific queries.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RentersController(
    IRenterService renterService,
    IRentService rentService,
    ILogger<RentersController> logger)
    : CrudControllerBase<RenterDto, RenterCreateUpdateDto, int>(renterService, logger)
{
    /// <summary>
    /// Get all rentals performed by a specific renter.
    /// </summary>
    /// <param name="id">Renter identifier.</param>
    /// <returns>List of RentalDto, or NoContent if none exist.</returns>
    [HttpGet("{id}/rents")]
    [ProducesResponseType(typeof(IList<RentDto>), 200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<RentDto>>> GetRents(int id)
    {
        logger.LogInformation("{method} called on {controller} with id={id}",
            nameof(GetRents), nameof(RentersController), id);

        try
        {
            var res = await rentService.GetRentsByBikeAsync(id);

            return res != null && res.Count > 0
                ? Ok(res)
                : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex,
                "Exception in {method} of {controller}",
                nameof(GetRents), nameof(RentersController));

            return StatusCode(500, ex.Message);
        }
    }
}
