using DeliveryApp.Application.Dto.Addresses;
using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Persistance;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.Addresses.GetAddress;

public class GetAddressHandler : IQueryHandler<GetAddress, GetAddressResponse>
{
    private readonly DeliveryDbContext _context;

    public GetAddressHandler(DeliveryDbContext deliveryDbContext)
    {
        _context = deliveryDbContext;
    }

    public async Task<GetAddressResponse> Handle(GetAddress request, CancellationToken cancellationToken)
    {
        var response = await _context.Address
            .Where(x => x.Id == request.AddressId)
            .Select(x => new GetAddressDto() 
            {
                AddressId = x.Id,
                Name = x.Name,
                CountryId = x.CountryId,
                PostCode = x.PostCode,
                City = x.City,
                Street = x.Street,
                Number = x.Number,
                AddressTypeId = x.AddressTypeId
            }).FirstOrDefaultAsync(cancellationToken);

        if (response == null)
            return new GetAddressResponse("No address was found under passed Id");

        return new GetAddressResponse()
        {
            Address = response
        };
    }
}
