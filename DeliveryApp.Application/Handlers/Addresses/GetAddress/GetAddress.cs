using DeliveryApp.Application.Interfaces.Mediator;

namespace DeliveryApp.Application.Handlers.Addresses.GetAddress;

public class GetAddress : IQuery<GetAddressResponse>
{
    public int AddressId { get; set; }
    public GetAddress(GetAddressParameters parameters)
    {
        AddressId = parameters.AddressId;
    }
}