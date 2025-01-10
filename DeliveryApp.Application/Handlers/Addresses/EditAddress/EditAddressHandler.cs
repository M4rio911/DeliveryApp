using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Persistance;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.Addresses.EditAddress;

public class EditAddressHandler : ICommandHandler<EditAddress, EditAddressResponse>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly DeliveryDbContext _context;

    public EditAddressHandler(IHttpContextAccessor httpContextAccessor, DeliveryDbContext deliveryDbContext)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = deliveryDbContext;
    }

    public async Task<EditAddressResponse> Handle(EditAddress request, CancellationToken cancellationToken)
    {
        var dbAddress = await _context.Address
            .Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
            
        if(dbAddress == null) 
            return new EditAddressResponse("No address was found under passed ID");

        var user = _httpContextAccessor.HttpContext?.User.Identities.FirstOrDefault().Name;
        if (user == null)
        {
            throw new UnauthorizedAccessException("User is not authenticated");
        }

        dbAddress.CountryId = request.CountryId;
        dbAddress.Name = request.Name;
        dbAddress.PostCode = request.PostCode;
        dbAddress.City = request.City;
        dbAddress.Street = request.Street;
        dbAddress.Number = request.Number;
        dbAddress.AddressTypeId = request.AddressTypeId;
        dbAddress.Modified = DateTime.UtcNow;
        dbAddress.ModifiedBy = user;

        await _context.SaveChangesAsync(cancellationToken);

        return new EditAddressResponse(dbAddress);
    }
}
