using BikeRental.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BikeRental.Api.Controllers;

/// <summary>
/// Generic base controller providing CRUD endpoints.
/// </summary>
/// <typeparam name="TDto">DTO used for GET responses.</typeparam>
/// <typeparam name="TCreateUpdateDto">DTO used for POST/PUT requests.</typeparam>
/// <typeparam name="TKey">Identifier type.</typeparam>
[Route("api/[controller]")]
[ApiController]
public abstract class CrudControllerBase<TDto, TCreateUpdateDto, TKey>(
    IApplicationService<TDto, TCreateUpdateDto, TKey> appService,
    ILogger<CrudControllerBase<TDto, TCreateUpdateDto, TKey>> logger) : ControllerBase
    where TDto : class
    where TCreateUpdateDto : class
    where TKey : struct
{
    /// <summary>
    /// Creates a new resource.
    /// </summary>
    /// <param name="newDto">DTO containing data for the new resource.</param>
    /// <returns>Created DTO with assigned identifier.</returns>
    /// <response code="201">Returns the newly created resource.</response>
    /// <response code="400">If validation fails.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<TDto>> Create(TCreateUpdateDto newDto)
    {
        logger.LogInformation("{Method} called on {Controller} with {@Dto}",
            nameof(Create), GetType().Name, newDto);
        try
        {
            var res = await appService.Create(newDto);
            logger.LogInformation("{Method} executed successfully on {Controller}",
                nameof(Create), GetType().Name);

            var idProp = res.GetType().GetProperty("Id");
            if (idProp != null)
            {
                var idValue = idProp.GetValue(res);
                if (idValue != null)
                {
                    return CreatedAtAction(nameof(Get), new { id = idValue }, res);
                }
            }

            return Created(string.Empty, res);
        }
        catch (ArgumentException argEx)
        {
            logger.LogWarning(argEx, "Validation failed in {Method} of {Controller}",
                nameof(Create), GetType().Name);
            return BadRequest(argEx.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception in {Method} of {Controller}",
                nameof(Create), GetType().Name);
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Updates an existing resource.
    /// </summary>
    /// <param name="id">Identifier of the resource to update.</param>
    /// <param name="newDto">DTO containing updated values.</param>
    /// <returns>Updated DTO.</returns>
    /// <response code="200">If the resource is updated successfully.</response>
    /// <response code="400">If validation fails.</response>
    /// <response code="404">If the resource is not found.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<TDto>> Edit(TKey id, TCreateUpdateDto newDto)
    {
        logger.LogInformation("{Method} called on {Controller} with id={Id}, {@Dto}",
            nameof(Edit), GetType().Name, id, newDto);
        try
        {
            var res = await appService.Update(newDto, id);

            if (res == null)
            {
                logger.LogWarning("{Entity} with id={Id} not found in {Method}",
                    typeof(TDto).Name, id, nameof(Edit));
                return NotFound($"{typeof(TDto).Name} with id={id} not found");
            }

            logger.LogInformation("{Method} executed successfully on {Controller}",
                nameof(Edit), GetType().Name);
            return Ok(res);
        }
        catch (ArgumentException argEx)
        {
            logger.LogWarning(argEx, "Validation failed in {Method} of {Controller}",
                nameof(Edit), GetType().Name);
            return BadRequest(argEx.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception in {Method} of {Controller}",
                nameof(Edit), GetType().Name);
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Deletes an existing resource.
    /// </summary>
    /// <param name="id">Identifier of the resource to delete.</param>
    /// <returns>HTTP status indicating outcome.</returns>
    /// <response code="200">If the resource is deleted successfully.</response>
    /// <response code="400">If validation fails.</response>
    /// <response code="404">If the resource is not found.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(TKey id)
    {
        logger.LogInformation("{Method} called on {Controller} with id={Id}",
            nameof(Delete), GetType().Name, id);
        try
        {
            await appService.Delete(id);
            logger.LogInformation("{Method} executed successfully on {Controller}",
                nameof(Delete), GetType().Name);
            return Ok();
        }
        catch (KeyNotFoundException)
        {
            logger.LogWarning("{Entity} with id={Id} not found in {Method}",
                typeof(TDto).Name, id, nameof(Delete));
            return NotFound($"{typeof(TDto).Name} with id={id} not found");
        }
        catch (ArgumentException argEx)
        {
            logger.LogWarning(argEx, "Validation failed in {Method} of {Controller}",
                nameof(Delete), GetType().Name);
            return BadRequest(argEx.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception in {Method} of {Controller}",
                nameof(Delete), GetType().Name);
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Retrieves all resources.
    /// </summary>
    /// <returns>List of DTOs representing all resources.</returns>
    /// <response code="200">Returns list of resources.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<TDto>>> GetAll()
    {
        logger.LogInformation("{Method} called on {Controller}",
            nameof(GetAll), GetType().Name);
        try
        {
            var res = await appService.GetAll();
            logger.LogInformation("{Method} executed successfully on {Controller}",
                nameof(GetAll), GetType().Name);
            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception in {Method} of {Controller}",
                nameof(GetAll), GetType().Name);
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Retrieves a single resource by identifier.
    /// </summary>
    /// <param name="id">Identifier of the resource to retrieve.</param>
    /// <returns>DTO if found; otherwise NotFound.</returns>
    /// <response code="200">Returns the requested resource.</response>
    /// <response code="404">If the resource is not found.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<TDto>> Get(TKey id)
    {
        logger.LogInformation("{Method} called on {Controller} with id={Id}",
            nameof(Get), GetType().Name, id);
        try
        {
            var res = await appService.Get(id);

            if (res == null)
            {
                logger.LogWarning("{Entity} with id={Id} not found",
                    typeof(TDto).Name, id);
                return NotFound($"{typeof(TDto).Name} with id={id} not found");
            }

            logger.LogInformation("{Method} executed successfully on {Controller}",
                nameof(Get), GetType().Name);
            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception in {Method} of {Controller}",
                nameof(Get), GetType().Name);
            return StatusCode(500, ex.Message);
        }
    }
}
