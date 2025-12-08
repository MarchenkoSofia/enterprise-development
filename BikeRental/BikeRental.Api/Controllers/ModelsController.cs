using BikeRental.Application.Contracts.Bike;
using BikeRental.Application.Contracts.Model;
using Microsoft.AspNetCore.Mvc;

namespace BikeRental.Api.Controllers;

/// <summary>
/// Controller for managing bicycle models and their related operations.
/// </summary>
/// <param name="modelService">Application service for bike models.</param>
/// <param name="logger">Logger instance.</param>
[ApiController]
[Route("api/[controller]")]
public class ModelsController(
    IModelService modelService,
    ILogger<ModelsController> logger)
    : CrudControllerBase<ModelDto, ModelCreateUpdateDto, int>(modelService, logger)
{
    /// <summary>
    /// Retrieves a list of all bicycles belonging to a specific model.
    /// </summary>
    /// <param name="id">The unique identifier of the model.</param>
    /// <returns>A list of bikes for the specified model.</returns>
    /// <response code="200">Returns the list of bikes.</response>
    /// <response code="204">If no bikes are found for this model.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet("{id}/bikes")]
    [ProducesResponseType(typeof(IList<BikeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IList<BikeDto>>> GetBikes(int id)
    {
        logger.LogInformation("{Method} called on {Controller} with id={Id}",
            nameof(GetBikes), nameof(ModelsController), id);

        try
        {
            var bikes = await modelService.GetBikesAsync(id);

            return bikes != null && bikes.Any()
                ? Ok(bikes)
                : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception in {Method} of {Controller}",
                nameof(GetBikes), nameof(ModelsController));

            return StatusCode(500, ex.Message);
        }
    }
}
