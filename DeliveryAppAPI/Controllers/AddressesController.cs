using DeliveryApp.Application.Handlers.Addresses.AddAddress;
using DeliveryApp.Application.Handlers.Addresses.EditAddress;
using DeliveryApp.Application.Handlers.Addresses.GetAddress;
using DeliveryApp.Application.Handlers.Addresses.GetUserAddresses;
using DeliveryApp.Application.Handlers.Addresses.RemoveAddress;
using DeliveryApp.Application.Handlers.Cars.RemoveCar;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.API.Controllers;

[Produces("application/json")]
[Route("adddresses/[controller]")]
[ApiController]
//[Authorize]
public class AddressesController : ControllerBase
{
    private readonly IMediator _mediator;

    public AddressesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("getAddress")]
    public async Task<IActionResult> GetAddress([FromQuery] GetAddressParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new GetAddress(parameters));
        return Ok(result);
    }

    [HttpGet]
    [Route("getUserAddresses")]
    public async Task<IActionResult> GetUserAddresses([FromQuery] GetUserAddressesParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new GetUserAddresses(parameters));
        return Ok(result);
    }

    [HttpPost]
    [Route("addAddress")]
    public async Task<IActionResult> AddAddress([FromBody] AddAddressParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new AddAddress(parameters));
        return Ok(result);
    }

    [HttpDelete]
    [Route("removeAddress")]
    public async Task<IActionResult> RemoveAddress([FromBody] RemoveAddressParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new RemoveAddress(parameters));
        return Ok(result);
    }

    [HttpPost]
    [Route("removeAddress")]
    public async Task<IActionResult> EditAddress([FromBody] EditAddressParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new EditAddress(parameters));
        return Ok(result);
    }
}
