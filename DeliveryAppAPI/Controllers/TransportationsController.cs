using DeliveryApp.Application.Handlers.Transportations.AssignPackageToTransportation;
using DeliveryApp.Application.Handlers.Transportations.GetDriverTransportations;
using DeliveryApp.Application.Handlers.Transportations.GetTransportation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.API.Controllers;

[Produces("application/json")]
[Route("[controller]")]
[ApiController]
[Authorize]
public class TransportationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TransportationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("getTransportations")]
    public async Task<IActionResult> GetTransportations([FromQuery] GetTransportationsParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new GetTransportations(parameters));
        return Ok(result);
    }

    [HttpGet]
    [Route("getDriverTransportations")]
    public async Task<IActionResult> GetDriverTransportations([FromQuery] GetDriverTransportationsParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new GetDriverTransportations(parameters));
        return Ok(result);
    }

    [HttpPost]
    [Route("assignPackageToTransportation")]
    public async Task<IActionResult> AssignPackageToTransportation([FromQuery] AssignPackageToTransportationParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new AssignPackageToTransportation(parameters));
        return Ok(result);
    }
}
