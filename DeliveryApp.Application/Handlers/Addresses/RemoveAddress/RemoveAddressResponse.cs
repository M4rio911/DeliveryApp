using DeliveryApp.Application.Handlers.BaseModel;

namespace DeliveryApp.Application.Handlers.Addresses.RemoveAddress;

public class RemoveAddressResponse : BaseResponse
{
    public RemoveAddressResponse() : base(true, null, true) { }
    public RemoveAddressResponse(string error) : base(error) { }
}
