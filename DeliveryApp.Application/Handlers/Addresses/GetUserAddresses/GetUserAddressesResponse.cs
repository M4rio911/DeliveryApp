using DeliveryApp.Application.Dto.Addresses;
using DeliveryApp.Application.Handlers.BaseModel;

namespace DeliveryApp.Application.Handlers.Addresses.GetUserAddresses;

public class GetUserAddressesResponse : BaseResponse
{
    public IEnumerable<GetAddressDto> UserAddresses { get; set; }

    public GetUserAddressesResponse() : base(true, null) { }
    public GetUserAddressesResponse(string error) : base(error) { }
}
