using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Persistance;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.Countries.EditCountry;

public class EditCountryHandler : ICommandHandler<EditCountry, EditCountryResponse>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly DeliveryDbContext _context;

    public EditCountryHandler(IHttpContextAccessor httpContextAccessor, DeliveryDbContext deliveryDbContext)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = deliveryDbContext;
    }

    public async Task<EditCountryResponse> Handle(EditCountry request, CancellationToken cancellationToken)
    {
        var dbCountry = await _context.Countries
            .Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
            
        if(dbCountry == null) 
            return new EditCountryResponse("No Country was found under passed ID");

        var user = _httpContextAccessor.HttpContext?.User.Identities.FirstOrDefault().Name;
        if (user == null)
        {
            throw new UnauthorizedAccessException("User is not authenticated");
        }

        dbCountry.Name = request.Name;
        dbCountry.Code = request.Code;
        dbCountry.CurrencyId = request.CurrencyId;
        dbCountry.Modified = DateTime.UtcNow;
        dbCountry.ModifiedBy = user;

        await _context.SaveChangesAsync(cancellationToken);

        return new EditCountryResponse(dbCountry);
    }
}
