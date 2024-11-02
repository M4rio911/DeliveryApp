using DeliveryApp.Application.Handlers.Cars.AddCar;
using DeliveryApp.Application.Handlers.Cars.EditCar;
using DeliveryApp.Application.Handlers.Cars.GetCar;
using DeliveryApp.Application.Handlers.Cars.GetCars;
using DeliveryApp.Application.Handlers.Cars.RemoveCar;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.API.Controllers;

[Produces("application/json")]
[Route("[controller]")]
[ApiController]
[Authorize]
public class CarsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CarsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("getCars")]
    public async Task<IActionResult> GetCars()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new GetCars());
        return Ok(result);
    }

    [HttpGet]
    [Route("getCar")]
    public async Task<IActionResult> GetCar([FromQuery] GetCarParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new GetCar(parameters));
        return Ok(result);
    }

    [HttpPost]
    [Route("addCar")]
    public async Task<IActionResult> AddCar([FromBody] AddCarParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new AddCar(parameters));
        return Ok(result);
    }

    [HttpDelete]
    [Route("removeCar")]
    public async Task<IActionResult> RemoveCar([FromBody] RemoveCarParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new RemoveCar(parameters));
        return Ok(result);
    }

    [HttpPost]
    [Route("editCar")]
    public async Task<IActionResult> EditCar([FromBody] EditCarParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new EditCar(parameters));
        return Ok(result);
    }
}
