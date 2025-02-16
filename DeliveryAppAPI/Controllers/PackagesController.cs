using DeliveryApp.Application.Handlers.Packages.AddPackage;
using DeliveryApp.Application.Handlers.Packages.GetPackages;
using DeliveryApp.Application.Handlers.Packages.GetUserPackages;
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
    public async Task<IActionResult> GetPackages()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new GetPackages());
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

    //[HttpGet]
    //[Route("getPackage")]
    //public async Task<IActionResult> GetPackage([FromQuery] GetPackageParameters parameters)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return BadRequest();
    //    }

    //    var result = await _mediator.Send(new GetPackage(parameters));
    //    return Ok(result);
    //}

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

    //[HttpDelete]
    //[Route("removePackage")]
    //public async Task<IActionResult> RemovePackage([FromBody] RemovePackageParameters parameters)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return BadRequest();
    //    }

    //    var result = await _mediator.Send(new RemovePackage(parameters));
    //    return Ok(result);
    //}

    //[HttpPost]
    //[Route("editPackage")]
    //public async Task<IActionResult> EditPackage([FromBody] EditPackageParameters parameters)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return BadRequest();
    //    }

    //    var result = await _mediator.Send(new EditPackage(parameters));
    //    return Ok(result);
    //}
}
