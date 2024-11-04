using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Persistance;

namespace DeliveryApp.Application.Handlers.Addresses.AddAddress;

public class AddAddressHandler : ICommandHandler<AddAddress, AddAddressResponse>
{
    private readonly DeliveryDbContext _context;

    public AddAddressHandler(DeliveryDbContext deliveryDbContext)
    {
        _context = deliveryDbContext;
    }

    public async Task<AddAddressResponse> Handle(AddAddress request, CancellationToken cancellationToken)
    {
        var newAddress = new Address
        {
            UserId = request.UserId,
            Name = request.Name,
            CountryId = request.CountryId,
            PostCode = request.PostCode,
            City = request.City,
            Street = request.Street,
            Number = request.Number,
            AddressTypeId = request.AddressTypeId,
            Created = DateTime.UtcNow,
            CreatedBy = "testUser",
            ModifiedBy = "testUser"
        };


        _context.Address.Add(newAddress);
        await _context.SaveChangesAsync(cancellationToken);

        return new AddAddressResponse();
    }
}
