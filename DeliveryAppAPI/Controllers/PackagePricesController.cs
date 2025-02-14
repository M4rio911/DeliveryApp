using DeliveryApp.Application.Handlers.PackagePrices.EditPackagePrices;
using DeliveryApp.Application.Handlers.PackagePrices.GetPackagePrices;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.API.Controllers;

[Produces("application/json")]
[Route("[controller]")]
[ApiController]
[Authorize]
public class PackagePricesController : ControllerBase
{
    private readonly IMediator _mediator;
    public PackagePricesController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("getPackagePrices")]
    public async Task<IActionResult> GetPackagePrices([FromQuery] GetPacakgePricesParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new GetPackagePrices(parameters.CurrencyId));
        return Ok(result);
    }

    [HttpPost]
    [Route("editPackagePrice")]
    public async Task<IActionResult> EditPackagePrice([FromBody] EditPackagePricesParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new EditPackagePrices(parameters));
        return Ok(result);
    }

    //[HttpGet]
    //[Route("getPackagePrice")]
    //public async Task<IActionResult> GetPackagePrice([FromQuery] GetPackagePriceParameters parameters)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return BadRequest();
    //    }

    //    var result = await _mediator.Send(new GetPackagePrice(parameters));
    //    return Ok(result);
    //}

    //[HttpPost]
    //[Route("addPackagePrice")]
    //public async Task<IActionResult> AddPackagePrice([FromBody] AddPackagePriceParameters parameters)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return BadRequest();
    //    }

    //    var result = await _mediator.Send(new AddPackagePrice(parameters));
    //    return Ok(result);
    //}

    //[HttpDelete]
    //[Route("removePackagePrice")]
    //public async Task<IActionResult> RemovePackagePrice([FromBody] RemovePackagePriceParameters parameters)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return BadRequest();
    //    }

    //    var result = await _mediator.Send(new RemovePackagePrice(parameters));
    //    return Ok(result);
    //}
}
