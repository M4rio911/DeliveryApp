using DeliveryApp.Application.Dto.Addresses;
using DeliveryApp.Application.Dto.Cars;
using DeliveryApp.Application.Handlers.BaseModel;

namespace DeliveryApp.Application.Handlers.Addresses.GetAddress;

public class GetAddressResponse : BaseResponse
{
    public GetAddressDto Address { get; set; }

    public GetAddressResponse() : base(true, null) { }
    public GetAddressResponse(string error) : base(error) { }
}
