using DeliveryApp.Application.Interfaces.Mediator;

namespace DeliveryApp.Application.Handlers.Addresses.GetUserAddresses;

public class GetUserAddresses : IQuery<GetUserAddressesResponse>
{
    public string UserId { get; set; }
    public GetUserAddresses(GetUserAddressesParameters parameters)
    {
        UserId = parameters.UserId;
    }
}