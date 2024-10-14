using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Persistance;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.Countries.EditCountry;

public class EditCountryHandler : ICommandHandler<EditCountry, EditCountryResponse>
{
    //private readonly 
    private readonly DeliveryDbContext _context;

    public EditCountryHandler(DeliveryDbContext deliveryDbContext)
    {
        _context = deliveryDbContext;
    }

    public async Task<EditCountryResponse> Handle(EditCountry request, CancellationToken cancellationToken)
    {
        var dbCountry = await _context.Countries
            .Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
            
        if(dbCountry == null) 
            return new EditCountryResponse("No Country was found under passed ID");

        dbCountry.Name = request.Name;
        dbCountry.Code = request.Code;
        dbCountry.CurrencyId = request.CurrencyId;
        dbCountry.Modified = DateTime.UtcNow;
        
        //User to do 
        dbCountry.ModifiedBy = "ModifiedUser";

        await _context.SaveChangesAsync(cancellationToken);

        return new EditCountryResponse(dbCountry);
    }
}
