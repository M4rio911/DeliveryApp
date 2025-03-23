using DeliveryApp.Application.Handlers.Transportations.AssignPackageToTransportation;
using DeliveryApp.Application.Handlers.Transportations.FinishTransportation;
using DeliveryApp.Application.Handlers.Transportations.GetDriverDailyTransportations;
using DeliveryApp.Application.Handlers.Transportations.GetDriverTransportations;
using DeliveryApp.Application.Handlers.Transportations.GetTransportation;
using DeliveryApp.Application.Handlers.Transportations.StartTransportation;
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

    [HttpGet]
    [Route("getDriverDailyTransportations")]
    public async Task<IActionResult> GetDriverDailyTransportations([FromQuery] GetDriverDailyTransportationsParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new GetDriverDailyTransportations(parameters));
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

    [HttpPost]
    [Route("startTransportation")]
    public async Task<IActionResult> StartTransportation([FromQuery] StartTransportationParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new StartTransportation(parameters));
        return Ok(result);
    }

    [HttpPost]
    [Route("finishTransportation")]
    public async Task<IActionResult> FinishTransportation([FromBody] FinishTransportationParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new FinishTransportation(parameters));
        return Ok(result);
    }
}