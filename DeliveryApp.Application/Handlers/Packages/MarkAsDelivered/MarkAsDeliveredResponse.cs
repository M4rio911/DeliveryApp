using DeliveryApp.Application.Dto.Countries;
using DeliveryApp.Application.Dto.Payments;
using DeliveryApp.Application.Handlers.BaseModel;
using DeliveryApp.Domain.Entities;

namespace DeliveryApp.Application.Handlers.Packages.MarkAsDelivered;

public class MarkAsDeliveredResponse : BaseResponse
{
    public MarkAsDeliveredResponse() : base(true, null) { }
    public MarkAsDeliveredResponse(string error) : base(error) { }
}
