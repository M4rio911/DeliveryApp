using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Persistance;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.Currencies.EditCurrency;

public class EditCurrencyHandler : ICommandHandler<EditCurrency, EditCurrencyResponse>
{
    //private readonly 
    private readonly DeliveryDbContext _context;

    public EditCurrencyHandler(DeliveryDbContext deliveryDbContext)
    {
        _context = deliveryDbContext;
    }

    public async Task<EditCurrencyResponse> Handle(EditCurrency request, CancellationToken cancellationToken)
    {
        var dbCurrency = await _context.Currencies
            .Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
            
        if(dbCurrency == null) 
            return new EditCurrencyResponse("No Currency was found under passed ID");

        dbCurrency.Shortcut = request.Shortcut;
        dbCurrency.Name = request.Name;
        dbCurrency.Modified = DateTime.UtcNow;
        
        //User to do 
        dbCurrency.ModifiedBy = "ModifiedUser";

        await _context.SaveChangesAsync(cancellationToken);

        return new EditCurrencyResponse(dbCurrency);
    }
}
