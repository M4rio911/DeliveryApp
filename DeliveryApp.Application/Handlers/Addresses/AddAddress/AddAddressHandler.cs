using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Persistance;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace DeliveryApp.Application.Handlers.Addresses.AddAddress;

public class AddAddressHandler : ICommandHandler<AddAddress, AddAddressResponse>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly DeliveryDbContext _context;

    public AddAddressHandler(IHttpContextAccessor httpContextAccessor, DeliveryDbContext deliveryDbContext)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = deliveryDbContext;
    }

    public async Task<AddAddressResponse> Handle(AddAddress request, CancellationToken cancellationToken)
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null)
        {
            throw new UnauthorizedAccessException("User is not authenticated");
        }
        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userName = user.Identities.FirstOrDefault().Name;

        var newAddress = new Address
        {
            UserId = userId,
            Name = request.Name,
            CountryId = request.CountryId,
            PostCode = request.PostCode,
            City = request.City,
            Street = request.Street,
            Number = request.Number,
            AddressTypeId = request.AddressTypeId,
            GuestAddress = false,
            Created = DateTime.UtcNow,
            CreatedBy = userName,
            ModifiedBy = userName
        };


        _context.Address.Add(newAddress);
        await _context.SaveChangesAsync(cancellationToken);

        return new AddAddressResponse();
    }
}
