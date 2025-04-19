using DeliveryApp.Application.Handlers.Countries.EditCountry;
using DeliveryApp.Application.Handlers.Drivers.EditDriver;
using DeliveryApp.Application.Handlers.Drivers.GetDriverByUserId;
using DeliveryApp.Application.Handlers.Drivers.GetDrivers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.API.Controllers;

[Produces("application/json")]
[Route("[controller]")]
[ApiController]
[Authorize]
public class DriversController : ControllerBase
{
    private readonly IMediator _mediator;

    public DriversController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("getDrivers")]
    public async Task<IActionResult> GetDrivers([FromQuery] GetDriversParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _mediator.Send(new GetDrivers(parameters));
        return Ok(result);
    }

    [HttpGet]
    [Route("getDriver")]
    public async Task<IActionResult> GetDriverByUserId([FromQuery] GetDriverParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _mediator.Send(new GetDriver(parameters));
        return Ok(result);
    }

    [HttpPost]
    [Route("editDriver")]
    public async Task<IActionResult> EditDriver([FromBody] EditDriverParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new EditDriver(parameters));
        return Ok(result);
    }
}