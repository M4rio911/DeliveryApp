using DeliveryApp.Application.Dto.Addresses;
using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Persistance;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.Addresses.GetUserAddresses;

public class GetUserAddressesHandler : IQueryHandler<GetUserAddresses, GetUserAddressesResponse>
{
    private readonly DeliveryDbContext _context;

    public GetUserAddressesHandler(DeliveryDbContext deliveryDbContext)
    {
        _context = deliveryDbContext;
    }

    public async Task<GetUserAddressesResponse> Handle(GetUserAddresses request, CancellationToken cancellationToken)
    {
        var response = await _context.Address
            .Where(x => x.UserId == request.UserId)
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
            }).ToListAsync(cancellationToken);

        return new GetUserAddressesResponse()
        {
            UserAddresses = response
        };
    }
}
