using DeliveryApp.Application.Dto.Payments;
using DeliveryApp.Application.Handlers.BaseModel;

namespace DeliveryApp.Application.Handlers.Payments.GetPayment;

public class GetPaymentResponse : BaseResponse
{
    public GetPaymentDto Payment { get; set; }

    public GetPaymentResponse() : base(true, null) { }
    public GetPaymentResponse(string error) : base(error) { }
}
