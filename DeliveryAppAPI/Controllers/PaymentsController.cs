using DeliveryApp.Application.Handlers.Payments.AddPayment;
using DeliveryApp.Application.Handlers.Payments.GetPayment;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DeliveryApp.Application.Handlers.Payments.GetPaymentByPackageId;
using DeliveryApp.Application.Handlers.Payments.SetPaymentAsPaid;

namespace DeliveryApp.API.Controllers;

[Produces("application/json")]
[Route("[controller]")]
[ApiController]
[Authorize]
public class PaymentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PaymentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("getPayment")]
    public async Task<IActionResult> GetPayment([FromQuery] GetPaymentParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new GetPayment(parameters));
        return Ok(result);
    }

    [HttpGet]
    [Route("getPaymentByPackageId")]
    public async Task<IActionResult> GetPaymentByPackageId([FromQuery] GetPaymentByPackageIdParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new GetPaymentByPackageId(parameters));
        return Ok(result);
    }

    [HttpPost]
    [Route("addPayment")]
    public async Task<IActionResult> AddPayment([FromBody] AddPaymentParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new AddPayment(parameters));
        return Ok(result);
    }

    [HttpPost]
    [Route("setPaymentAsPaid")]
    public async Task<IActionResult> SetPaymentAsPaid([FromBody] SetPaymentAsPaidParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new SetPaymentAsPaid(parameters));
        return Ok(result);
    }

    //[HttpDelete]
    //[Route("removePayment")]
    //public async Task<IActionResult> RemovePayment([FromBody] RemovePaymentParameters parameters)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return BadRequest();
    //    }

    //    var result = await _mediator.Send(new RemovePayment(parameters));
    //    return Ok(result);
    //}

    //[HttpPost]
    //[Route("editPayment")]
    //public async Task<IActionResult> EditPayment([FromBody] EditPaymentParameters parameters)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return BadRequest();
    //    }

    //    var result = await _mediator.Send(new EditPayment(parameters));
    //    return Ok(result);
    //}
}
