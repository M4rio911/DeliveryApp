using DeliveryApp.Application.Dto.Countries;
using DeliveryApp.Application.Dto.Payments;
using DeliveryApp.Application.Handlers.BaseModel;
using DeliveryApp.Domain.Entities;

namespace DeliveryApp.Application.Handlers.Packages.MarkAsCollected;

public class MarkAsCollectedResponse : BaseResponse
{
    public MarkAsCollectedResponse() : base(true, null, true) { }
    public MarkAsCollectedResponse(string error) : base(error) { }
}
