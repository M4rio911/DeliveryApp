using DeliveryApp.Application.Handlers.Cars.GetCar;
using DeliveryApp.Application.Handlers.Cars.RemoveCar;
using DeliveryApp.Application.Handlers.Dictionaries.AddDictionary;
using DeliveryApp.Application.Handlers.Dictionaries.AddDictionaryType;
using DeliveryApp.Application.Handlers.Dictionaries.GetDictionary;
using DeliveryApp.Application.Handlers.Dictionaries.GetDictionaryType;
using DeliveryApp.Application.Handlers.Dictionaries.RemoveDictionary;
using DeliveryApp.Application.Handlers.Dictionaries.RemoveDictionaryType;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.API.Controllers;

[Produces("application/json")]
[Route("dictionaries/[controller]")]
[ApiController]

public class DictionariesController : ControllerBase
{
    private readonly IMediator _mediator;

    public DictionariesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("getDictionary")]
    public async Task<IActionResult> getDictionary([FromQuery] GetDictionaryParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new GetDictionary(parameters));
        return Ok(result);
    }

    [HttpPost]
    [Route("addDictionary")]
    public async Task<IActionResult> addDictionary([FromBody] AddDictionaryParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new AddDictionary(parameters));
        return Ok(result);
    }

    [HttpDelete]
    [Route("removeDictionary")]
    public async Task<IActionResult> removeDictionary([FromBody] RemoveDictionaryParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new RemoveDictionary(parameters));
        return Ok(result);
    }

    [HttpGet]
    [Route("getDictionaryType")]
    public async Task<IActionResult> getDictionaryType([FromQuery] GetDictionaryTypeParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new GetDictionaryType(parameters));
        return Ok(result);
    }

    [HttpPost]
    [Route("addDictionaryType")]
    public async Task<IActionResult> addDictionaryType([FromBody] AddDictionaryTypeParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new AddDictionaryType(parameters));
        return Ok(result);
    }

    [HttpDelete]
    [Route("removeDictionaryType")]
    public async Task<IActionResult> removeDictionaryType([FromBody] RemoveDictionaryTypeParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new RemoveDictionaryType(parameters));
        return Ok(result);
    }
}
