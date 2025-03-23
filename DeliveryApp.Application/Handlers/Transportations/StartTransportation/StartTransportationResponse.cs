using DeliveryApp.Application.Handlers.BaseModel;

namespace DeliveryApp.Application.Handlers.Transportations.StartTransportation;

public class StartTransportationResponse : BaseResponse
{
    public StartTransportationResponse() : base(true, null) { }
    public StartTransportationResponse(string error) : base(error) { }
}
