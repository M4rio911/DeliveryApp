using DeliveryApp.Application.Handlers.BaseModel;

namespace DeliveryApp.Application.Handlers.Payments.AddPayment;

public class AddPaymentResponse : BaseResponse
{
    public AddPaymentResponse() : base(true, null, true) { }
    public AddPaymentResponse(List<string> errors) : base(errors) { }
}
