using DeliveryApp.Application.Handlers.Countries.AddCountry;
using DeliveryApp.Application.Handlers.Countries.EditCountry;
using DeliveryApp.Application.Handlers.Countries.GetCountries;
using DeliveryApp.Application.Handlers.Countries.GetCountry;
using DeliveryApp.Application.Handlers.Countries.RemoveCountry;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.API.Controllers;

[Produces("application/json")]
[Route("[controller]")]
[ApiController]
[Authorize]
public class CountriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CountriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("getCountries")]
    public async Task<IActionResult> GetCountries()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new GetCountries());
        return Ok(result);
    }

    [HttpGet]
    [Route("getCountry")]
    public async Task<IActionResult> GetCountry([FromQuery] GetCountryParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new GetCountry(parameters));
        return Ok(result);
    }

    [HttpPost]
    [Route("addCountry")]
    public async Task<IActionResult> AddCountry([FromBody] AddCountryParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new AddCountry(parameters));
        return Ok(result);
    }

    [HttpDelete]
    [Route("removeCountry")]
    public async Task<IActionResult> RemoveCountry([FromBody] RemoveCountryParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new RemoveCountry(parameters));
        return Ok(result);
    }

    [HttpPost]
    [Route("editCountry")]
    public async Task<IActionResult> EditCountry([FromBody] EditCountryParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new EditCountry(parameters));
        return Ok(result);
    }
}
