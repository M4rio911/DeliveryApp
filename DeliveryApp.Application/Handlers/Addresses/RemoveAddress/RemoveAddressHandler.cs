using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Persistance;

namespace DeliveryApp.Application.Handlers.Addresses.RemoveAddress;

public class RemoveAddressHandler : ICommandHandler<RemoveAddress, RemoveAddressResponse>
{
    private readonly DeliveryDbContext _context;

    public RemoveAddressHandler(DeliveryDbContext deliveryDbContext)
    {
        _context = deliveryDbContext;
    }

    public async Task<RemoveAddressResponse> Handle(RemoveAddress request, CancellationToken cancellationToken)
    {
        var addressToRemove = _context.Address
            .FirstOrDefault(x => x.Id == request.AddressId);

        if (addressToRemove == null)
        {
            return new RemoveAddressResponse("Address with passed Id does not exists");
        }

        _context.Address.Remove(addressToRemove);
        await _context.SaveChangesAsync(cancellationToken);

        return new RemoveAddressResponse();
    }
}
