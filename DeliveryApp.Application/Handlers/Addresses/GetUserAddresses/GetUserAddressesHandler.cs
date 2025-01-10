using DeliveryApp.Application.Dto.Addresses;
using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Persistance;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DeliveryApp.Application.Handlers.Addresses.GetUserAddresses;

public class GetUserAddressesHandler : IQueryHandler<GetUserAddresses, GetUserAddressesResponse>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly DeliveryDbContext _context;

    public GetUserAddressesHandler(IHttpContextAccessor httpContextAccessor, DeliveryDbContext deliveryDbContext)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = deliveryDbContext;
    }

    public async Task<GetUserAddressesResponse> Handle(GetUserAddresses request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            throw new UnauthorizedAccessException("User is not authenticated");
        }

        var response = await _context.Address
            .Where(x => x.UserId == userId)
            .Select(x => new GetAddressDto() 
            {
                Id = x.Id,
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
