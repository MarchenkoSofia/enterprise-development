using BikeRental.Application.Contracts.Bike;
using BikeRental.Application.Contracts.Rent;
using Microsoft.AspNetCore.Mvc;

namespace BikeRental.Api.Controllers;

/// <summary>
/// Controller for bike CRUD operations and additional bike-related queries.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class BikesController(
    IBikeService bikeService,
    IRentService rentService,
    ILogger<BikesController> logger)
    : CrudControllerBase<BikeDto, BikeCreateUpdateDto, int>(bikeService, logger)
{
    /// <summary>
    /// Get all rentals associated with a specific bike.
    /// </summary>
    /// <param name="id">Identifier of the bike.</param>
    /// <returns>List of rentals for the bike, or NoContent if none exist.</returns>
    [HttpGet("{id}/rents")]
    [ProducesResponseType(typeof(IList<RentDto>), 200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<RentDto>>> GetRents(int id)
    {
        logger.LogInformation("{method} called on {controller} with id={id}",
            nameof(GetRents), nameof(BikesController), id);

        try
        {
            var rents = await rentService.GetRentsByBikeAsync(id);

            return rents != null && rents.Count > 0
                ? Ok(rents)
                : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex,
                "Exception in {method} of {controller}",
                nameof(GetRents), nameof(BikesController));

            return StatusCode(500, ex.Message);
        }
    }
}
