using DeliveryApp.Application.Dto.Currencies;
using DeliveryApp.Application.Dto.Users;
using DeliveryApp.Application.Handlers.BaseModel;

namespace DeliveryApp.Application.Handlers.Drivers.GetDriverByUserId;

public class GetDriverResponse : BaseResponse
{
    public GetDriverDto Driver { get; set; }

    public GetDriverResponse() : base(true, null) { }
    public GetDriverResponse(List<string> errors) : base(errors) { }
}
