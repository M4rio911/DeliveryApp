using DeliveryApp.Application.Dto.Countries;
using DeliveryApp.Application.Dto.Payments;
using DeliveryApp.Application.Handlers.BaseModel;
using DeliveryApp.Domain.Entities;

namespace DeliveryApp.Application.Handlers.Payments.SetPaymentAsPaid;

public class SetPaymentAsPaidResponse : BaseResponse
{
    public SetPaymentAsPaidResponse() : base(true, null) { }
    public SetPaymentAsPaidResponse(string error) : base(error) { }
}
