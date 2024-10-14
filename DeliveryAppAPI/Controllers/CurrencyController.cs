using DeliveryApp.Application.Handlers.Currencies.AddCurrency;
using DeliveryApp.Application.Handlers.Currencies.EditCurrency;
using DeliveryApp.Application.Handlers.Currencies.GetCurrencies;
using DeliveryApp.Application.Handlers.Currencies.GetCurrency;
using DeliveryApp.Application.Handlers.Currencies.RemoveCurrency;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.API.Controllers;

[Produces("application/json")]
[Route("[controller]")]
[ApiController]

public class CurrencyController : ControllerBase
{
    private readonly IMediator _mediator;

    public CurrencyController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("getCurrencies")]
    public async Task<IActionResult> GetCurrencies()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new GetCurrencies());
        return Ok(result);
    }

    [HttpGet]
    [Route("getCurrency")]
    public async Task<IActionResult> GetCurrency([FromQuery] GetCurrencyParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new GetCurrency(parameters));
        return Ok(result);
    }

    [HttpPost]
    [Route("addCurrency")]
    public async Task<IActionResult> AddCurrency([FromBody] AddCurrencyParamenters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new AddCurrency(parameters));
        return Ok(result);
    }

    [HttpDelete]
    [Route("removeCurrency")]
    public async Task<IActionResult> RemoveCurrency([FromBody] RemoveCurrencyParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new RemoveCurrency(parameters));
        return Ok(result);
    }

    [HttpPost]
    [Route("editCurrency")]
    public async Task<IActionResult> EditCurrency([FromBody] EditCurrencyParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new EditCurrency(parameters));
        return Ok(result);
    }
}
