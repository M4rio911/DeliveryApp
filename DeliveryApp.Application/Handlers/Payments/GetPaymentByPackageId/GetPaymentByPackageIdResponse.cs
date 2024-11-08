using DeliveryApp.Application.Dto.Payments;
using DeliveryApp.Application.Handlers.BaseModel;

namespace DeliveryApp.Application.Handlers.Payments.GetPaymentByPackageId;

public class GetPaymentByPackageIdResponse : BaseResponse
{
    public GetPaymentDto Payment { get; set; }

    public GetPaymentByPackageIdResponse() : base(true, null) { }
    public GetPaymentByPackageIdResponse(string error) : base(error) { }
}
