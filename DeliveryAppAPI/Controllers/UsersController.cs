using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.API.Controllers;

[Produces("application/json")]
[Route("users/[controller]")]
[ApiController]

public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("getAllUsers")]
    public async Task<IActionResult> GetAllUsers()
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        //var result = await _mediator.Send(new GetAllUsers());
        return Ok();
    }
}
