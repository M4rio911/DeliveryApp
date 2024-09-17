﻿using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.API.Controllers;

[Produces("application/json")]
[Route("cars/[controller]")]
[ApiController]

public class CarsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CarsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("getCar")]
    public async Task<IActionResult> GetCar()
    {
        return Ok();
    }

    [HttpPost]
    [Route("addCar")]
    public async Task<IActionResult> AddCar()
    {
        return Ok();
    }

    [HttpDelete]
    [Route("removeCar")]
    public async Task<IActionResult> RemoveCar()
    {
        return Ok();
    }

    [HttpPost]
    [Route("removeCar")]
    public async Task<IActionResult> EditCar()
    {
        return Ok();
    }
}