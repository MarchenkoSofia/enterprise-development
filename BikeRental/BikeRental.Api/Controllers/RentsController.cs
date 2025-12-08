using BikeRental.Application.Contracts.Rent;

namespace BikeRental.Api.Controllers;

/// <summary>
/// Controller for rental CRUD operations.
/// </summary>
/// <param name="service">Application service for rentals.</param>
/// <param name="logger">Logger instance.</param>
public class RentsController(
    IRentService service,
    ILogger<RentsController> logger)
    : CrudControllerBase<RentDto, RentCreateUpdateDto, int>(service, logger)
{
}
