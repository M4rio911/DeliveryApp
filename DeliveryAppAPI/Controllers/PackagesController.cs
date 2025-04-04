using DeliveryApp.Application.Handlers.Packages.GetPackageDetails;
using DeliveryApp.Application.Handlers.Packages.GetUserPackages;
using DeliveryApp.Application.Handlers.Packages.MarkAsCollected;
using DeliveryApp.Application.Handlers.Packages.MarkAsDelivered;
using DeliveryApp.Application.Handlers.Packages.CollectPackage;
using DeliveryApp.Application.Handlers.Packages.AssignPackage;
using DeliveryApp.Application.Handlers.Packages.GetPackages;
using DeliveryApp.Application.Handlers.Packages.SendPackage;
using DeliveryApp.Application.Handlers.Packages.AddPackage;
using DeliveryApp.Application.Handlers.Packages.GetPackage;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.API.Controllers;

[Produces("application/json")]
[Route("[controller]")]
[ApiController]
[Authorize]
public class PackagesController : ControllerBase
{
    private readonly IMediator _mediator;

    public PackagesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("getPackages")]
    public async Task<IActionResult> GetPackages([FromQuery] GetPackagesParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new GetPackages(parameters));
        return Ok(result);
    }

    [HttpGet]
    [Route("getUserPackages")]
    public async Task<IActionResult> GetUserPackages()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new GetUserPackages());
        return Ok(result);
    }

    [HttpGet]
    [Route("getPackage")]
    public async Task<IActionResult> GetPackage([FromQuery] GetPackageParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new GetPackage(parameters));
        return Ok(result);
    }

    [HttpGet]
    [Route("getPackageDetails")]
    public async Task<IActionResult> GetPackageDetails([FromQuery] GetPackageDetailsParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new GetPackageDetails(parameters));
        return Ok(result);
    }

    [HttpPost]
    [Route("addPackage")]
    public async Task<IActionResult> AddPackage([FromBody] AddPackageParamenters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new AddPackage(parameters));
        return Ok(result);
    }

    [HttpPost]
    [Route("assignPackage")]
    public async Task<IActionResult> AssignPackage([FromBody] AssignPackageParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new AssignPackage(parameters));
        return Ok(result);
    }

    [HttpPost]
    [Route("sendPackage")]
    public async Task<IActionResult> SendPackage([FromBody] SendPackageParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new SendPackage(parameters));
        return Ok(result);
    }

    [HttpPost]
    [Route("collectPackage")]
    public async Task<IActionResult> CollectPackage([FromBody] CollectPackageParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new CollectPackage(parameters));
        return Ok(result);
    }

    [HttpPost]
    [Route("markAsCollected")]
    public async Task<IActionResult> MarkAsCollected([FromBody] MarkAsCollectedParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new MarkAsCollected(parameters));
        return Ok(result);
    }

    [HttpPost]
    [Route("MarkAsDelivered")]
    public async Task<IActionResult> MarkAsDeliveried([FromBody] MarkAsDeliveredParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new MarkAsDelivered(parameters));
        return Ok(result);
    }
}
