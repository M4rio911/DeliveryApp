using DeliveryApp.Application.Handlers.BaseModel;

namespace DeliveryApp.Application.Handlers.Addresses.AddAddress;

public class AddAddressResponse : BaseResponse
{
    public AddAddressResponse() : base(true, null, true) { }
    public AddAddressResponse(List<string> errors) : base(errors) { }
}
