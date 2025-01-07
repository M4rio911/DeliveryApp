using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Persistance;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.Currencies.EditCurrency;

public class EditCurrencyHandler : ICommandHandler<EditCurrency, EditCurrencyResponse>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly DeliveryDbContext _context;

    public EditCurrencyHandler(IHttpContextAccessor httpContextAccessor, DeliveryDbContext deliveryDbContext)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = deliveryDbContext;
    }

    public async Task<EditCurrencyResponse> Handle(EditCurrency request, CancellationToken cancellationToken)
    {
        var dbCurrency = await _context.Currencies
            .Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
            
        if(dbCurrency == null) 
            return new EditCurrencyResponse("No Currency was found under passed ID");

        var user = _httpContextAccessor.HttpContext?.User.Identities.FirstOrDefault().Name;
        if (user == null)
        {
            throw new UnauthorizedAccessException("User is not authenticated");
        }

        dbCurrency.Shortcut = request.Shortcut;
        dbCurrency.Name = request.Name;
        dbCurrency.Modified = DateTime.UtcNow;
        dbCurrency.ModifiedBy = user;

        await _context.SaveChangesAsync(cancellationToken);

        return new EditCurrencyResponse(dbCurrency);
    }
}
