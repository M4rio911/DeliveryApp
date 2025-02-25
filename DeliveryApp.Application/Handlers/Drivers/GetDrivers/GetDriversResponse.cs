using DeliveryApp.Application.Dto.Currencies;
using DeliveryApp.Application.Dto.Users;
using DeliveryApp.Application.Handlers.BaseModel;

namespace DeliveryApp.Application.Handlers.Drivers.GetDrivers;

public class GetDriversResponse : BaseResponse
{
    public List<GetDriverDto> Drivers { get; set; }

    public GetDriversResponse() : base(true, null) { }
    public GetDriversResponse(List<string> errors) : base(errors) { }
}
