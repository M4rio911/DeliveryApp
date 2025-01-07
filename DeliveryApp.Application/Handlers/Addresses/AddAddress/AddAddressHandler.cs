using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Persistance;
using Microsoft.AspNetCore.Http;

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
        var user = _httpContextAccessor.HttpContext?.User.Identities.FirstOrDefault().Name;

        if (user == null)
        {
            throw new UnauthorizedAccessException("User is not authenticated");
        }

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
            CreatedBy = user,
            ModifiedBy = user
        };


        _context.Address.Add(newAddress);
        await _context.SaveChangesAsync(cancellationToken);

        return new AddAddressResponse();
    }
}
