using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Persistance;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.Addresses.EditAddress;

public class EditAddressHandler : ICommandHandler<EditAddress, EditAddressResponse>
{
    //private readonly 
    private readonly DeliveryDbContext _context;

    public EditAddressHandler(DeliveryDbContext deliveryDbContext)
    {
        _context = deliveryDbContext;
    }

    public async Task<EditAddressResponse> Handle(EditAddress request, CancellationToken cancellationToken)
    {
        var dbAddress = await _context.Address
            .Where(x => x.Id == request.AddressId)
            .FirstOrDefaultAsync(cancellationToken);
            
        if(dbAddress == null) 
            return new EditAddressResponse("No address was found under passed ID");

        dbAddress.CountryId = request.CountryId;
        dbAddress.Name = request.Name;
        dbAddress.PostCode = request.PostCode;
        dbAddress.City = request.City;
        dbAddress.Street = request.Street;
        dbAddress.Number = request.Number;
        dbAddress.AddressTypeId = request.AddressTypeId;
        dbAddress.Modified = DateTime.UtcNow;
        
        //User to do 
        dbAddress.ModifiedBy = "ModifiedUser";

        await _context.SaveChangesAsync(cancellationToken);

        return new EditAddressResponse(dbAddress);
    }
}
