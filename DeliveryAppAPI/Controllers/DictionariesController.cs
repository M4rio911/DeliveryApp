using DeliveryApp.Application.Handlers.Cars.RemoveCar;
using DeliveryApp.Application.Handlers.Dictionaries.AddDictionaryType;
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
    public async Task<IActionResult> getDictionary()
    {
        return Ok();
    }

    [HttpPost]
    [Route("addDictionary")]
    public async Task<IActionResult> addDictionary()
    {
        return Ok();
    }

    [HttpDelete]
    [Route("removeDictionary")]
    public async Task<IActionResult> removeDictionary()
    {
        return Ok();
    }

    [HttpPost]
    [Route("removeDictionary")]
    public async Task<IActionResult> editDictionary()
    {
        return Ok();
    }

    [HttpGet]
    [Route("getDictionaryType")]
    public async Task<IActionResult> getDictionaryType()
    {
        return Ok();
    }

    [HttpPost]
    [Route("addDictionaryType")]
    public async Task<IActionResult> addDictionaryType(AddDictionaryTypeParameters parameters)
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
    public async Task<IActionResult> removeDictionaryType()
    {
        return Ok();
    }

    [HttpPost]
    [Route("removeDictionaryType")]
    public async Task<IActionResult> editDictionaryType()
    {
        return Ok();
    }
}
