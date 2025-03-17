using DeliveryApp.Application.Dto.Payments;
using DeliveryApp.Application.Dto.Transportations;
using DeliveryApp.Application.Handlers.BaseModel;

namespace DeliveryApp.Application.Handlers.Transportations.GetDriverTransportations;

public class GetDriverTransportationsResponse : BaseResponse
{
    public GetDriverTransportationsDto Transportation { get; set; }

    public GetDriverTransportationsResponse() : base(true, null) { }
    public GetDriverTransportationsResponse(string error) : base(error) { }
}
