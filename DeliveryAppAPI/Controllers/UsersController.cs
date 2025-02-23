using DeliveryApp.Application.Handlers.User.ChangeActiveStatus;
using DeliveryApp.Application.Handlers.User.EditUser;
using DeliveryApp.Application.Handlers.Users.GetAllUsers;
using DeliveryApp.Application.Handlers.Users.GetUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.API.Controllers;

[Produces("application/json")]
[Route("[controller]")]
[ApiController]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("getAllUsers")]
    public async Task<IActionResult> GetAllUsers([FromQuery] GetAllUsersParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _mediator.Send(new GetAllUsers(parameters));
        return Ok(result);
    }

    [HttpGet]
    [Route("getUser")]
    public async Task<IActionResult> GetUser([FromQuery] GetUserParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _mediator.Send(new GetUser(parameters));
        return Ok(result);
    }

    [HttpPost]
    [Route("editUser")]
    public async Task<IActionResult> EditUser([FromBody] EditUserParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _mediator.Send(new EditUser(parameters));
        return Ok(result);
    }

    [HttpPost]
    [Route("changeActiveStatus")]
    public async Task<IActionResult> ChangeActiveStatus([FromBody] ChangeActiveStatusParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _mediator.Send(new ChangeActiveStatus(parameters));
        return Ok(result);
    }
}