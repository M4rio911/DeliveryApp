using static DeliveryApp.Application.Interfaces.Mediator.ICommand;

namespace DeliveryApp.Application.Handlers.Addresses.RemoveAddress;

public class RemoveAddress : ICommand<RemoveAddressResponse>
{
    public int AddressId { get; set; }

    public RemoveAddress(RemoveAddressParameters parameters)
    {
        AddressId = parameters.AddressId;
    }
}