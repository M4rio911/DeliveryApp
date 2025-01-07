using DeliveryApp.Application.Handlers.Dictionaries.AddDictionary;
using DeliveryApp.Application.Handlers.Dictionaries.AddDictionaryType;
using DeliveryApp.Application.Handlers.Dictionaries.EditDictionary;
using DeliveryApp.Application.Handlers.Dictionaries.EditDictionaryType;
using DeliveryApp.Application.Handlers.Dictionaries.GetDictionariesByType;
using DeliveryApp.Application.Handlers.Dictionaries.GetDictionary;
using DeliveryApp.Application.Handlers.Dictionaries.GetDictionaryType;
using DeliveryApp.Application.Handlers.Dictionaries.GetDictionaryTypes;
using DeliveryApp.Application.Handlers.Dictionaries.RemoveDictionary;
using DeliveryApp.Application.Handlers.Dictionaries.RemoveDictionaryType;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.API.Controllers;

[Produces("application/json")]
[Route("[controller]")]
[ApiController]
[Authorize]
public class DictionariesController : ControllerBase
{
    private readonly IMediator _mediator;

    public DictionariesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("getDictionary")]
    public async Task<IActionResult> GetDictionary([FromQuery] GetDictionaryParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new GetDictionary(parameters));
        return Ok(result);
    }

    [HttpGet]
    [Route("getDictionariesByType")]
    public async Task<IActionResult> GetDictionariesByType([FromQuery] GetDictionariesByTypeParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new GetDictionariesByType(parameters));
        return Ok(result);
    }

    [HttpPost]
    [Route("addDictionary")]
    public async Task<IActionResult> AddDictionary([FromBody] AddDictionaryParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new AddDictionary(parameters));
        return Ok(result);
    }

    [HttpPost]
    [Route("editDictionary")]
    public async Task<IActionResult> EditDictionary([FromBody] EditDictionaryParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new EditDictionary(parameters));
        return Ok(result);
    }

    [HttpDelete]
    [Route("removeDictionary")]
    public async Task<IActionResult> RemoveDictionary([FromBody] RemoveDictionaryParameters parameters)
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
    public async Task<IActionResult> GetDictionaryType([FromQuery] GetDictionaryTypeParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new GetDictionaryType(parameters));
        return Ok(result);
    }

    [HttpGet]
    [Route("getDictionaryTypes")]
    public async Task<IActionResult> GetDictionaryTypes()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new GetDictionaryTypes());
        return Ok(result);
    }

    [HttpPost]
    [Route("addDictionaryType")]
    public async Task<IActionResult> AddDictionaryType([FromBody] AddDictionaryTypeParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new AddDictionaryType(parameters));
        return Ok(result);
    }

    [HttpPost]
    [Route("editDictionaryType")]
    public async Task<IActionResult> EditDictionaryType([FromBody] EditDictionaryTypeParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new EditDictionaryType(parameters));
        return Ok(result);
    }

    [HttpDelete]
    [Route("removeDictionaryType")]
    public async Task<IActionResult> RemoveDictionaryType([FromBody] RemoveDictionaryTypeParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new RemoveDictionaryType(parameters));
        return Ok(result);
    }
}
