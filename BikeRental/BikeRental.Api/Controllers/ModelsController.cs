using BikeRental.Application.Contracts.Model;
using BikeRental.Application.Contracts.Bike;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BikeRental.Api.Controllers;

/// <summary>
/// Controller for bicycle model CRUD operations and model-related queries.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ModelController(
    IModelService modelService,
    ILogger<ModelController> logger)
    : CrudControllerBase<ModelDto, ModelCreateUpdateDto, int>(modelService, logger)
{
    /// <summary>
    /// Get a list of bicycles that belong to a specific model.
    /// </summary>
    /// <param name="id">Model identifier.</param>
    /// <returns>List of bicycles of the model, or NoContent if not found.</returns>
    [HttpGet("{id}/bikes")]
    [ProducesResponseType(typeof(IList<BikeDto>), 200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<BikeDto>>> GetBikes(int id)
    {
        logger.LogInformation("{method} called on {controller} with id={id}",
            nameof(GetBikes), nameof(ModelController), id);

        try
        {
            var bikes = await modelService.GetBikesAsync(id);

            return bikes != null && bikes.Count > 0
                ? Ok(bikes)
                : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex,
                "Exception in {method} of {controller}",
                nameof(GetBikes), nameof(ModelController));

            return StatusCode(500, ex.Message);
        }
    }
}
