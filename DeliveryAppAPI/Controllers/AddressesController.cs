using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.API.Controllers;

[Produces("application/json")]
[Route("adddresses/[controller]")]
[ApiController]
[Authorize]
public class AddressesController : ControllerBase
{
    private readonly IMediator _mediator;

    public AddressesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("getAddress")]
    public async Task<IActionResult> GetAddress()
    {
        return Ok();
    }

    [HttpPost]
    [Route("addAddress")]
    public async Task<IActionResult> AddAddress()
    {
        return Ok();
    }

    [HttpDelete]
    [Route("removeAddress")]
    public async Task<IActionResult> RemoveAddress()
    {
        return Ok();
    }

    [HttpPost]
    [Route("removeAddress")]
    public async Task<IActionResult> EditAddress()
    {
        return Ok();
    }
}
