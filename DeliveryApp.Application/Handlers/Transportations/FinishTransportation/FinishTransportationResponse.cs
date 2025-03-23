using DeliveryApp.Application.Handlers.BaseModel;

namespace DeliveryApp.Application.Handlers.Transportations.FinishTransportation;

public class FinishTransportationResponse : BaseResponse
{
    public FinishTransportationResponse() : base(true, null) { }
    public FinishTransportationResponse(string error) : base(error) { }
}
