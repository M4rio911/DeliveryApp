using DeliveryApp.Application.Dto.Payments;
using DeliveryApp.Application.Dto.Transportations;
using DeliveryApp.Application.Handlers.BaseModel;

namespace DeliveryApp.Application.Handlers.Transportations.GetDriverDailyTransportations;

public class GetDriverDailyTransportationsResponse : BaseResponse
{
    public GetDriverTransportationsDto Transportation { get; set; }

    public GetDriverDailyTransportationsResponse() : base(true, null) { }
    public GetDriverDailyTransportationsResponse(string error) : base(error) { }
}
