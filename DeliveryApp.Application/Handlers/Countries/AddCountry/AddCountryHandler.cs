using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Persistance;
using Microsoft.AspNetCore.Http;

namespace DeliveryApp.Application.Handlers.Countries.AddCountry;

public class AddCountryHandler : ICommandHandler<AddCountry, AddCountryResponse>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly DeliveryDbContext _context;

    public AddCountryHandler(IHttpContextAccessor httpContextAccessor, DeliveryDbContext deliveryDbContext)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = deliveryDbContext;
    }

    public async Task<AddCountryResponse> Handle(AddCountry request, CancellationToken cancellationToken)
    {
        var user = _httpContextAccessor.HttpContext?.User.Identities.FirstOrDefault().Name;
        if (user == null)
        {
            throw new UnauthorizedAccessException("User is not authenticated");
        }

        var newCountry = new Country
        {
            Name = request.Name,
            Code = request.Code,
            CurrencyId = request.CurrencyId,
            Created = DateTime.UtcNow,
            CreatedBy = user,
            ModifiedBy = user
        };

        _context.Countries.Add(newCountry);
        await _context.SaveChangesAsync(cancellationToken);

        return new AddCountryResponse();
    }
}
