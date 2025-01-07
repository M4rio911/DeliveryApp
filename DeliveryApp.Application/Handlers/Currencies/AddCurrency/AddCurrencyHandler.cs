using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Persistance;
using Microsoft.AspNetCore.Http;

namespace DeliveryApp.Application.Handlers.Currencies.AddCurrency;

public class AddCurrencyHandler : ICommandHandler<AddCurrency, AddCurrencyResponse>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly DeliveryDbContext _context;

    public AddCurrencyHandler(IHttpContextAccessor httpContextAccessor, DeliveryDbContext deliveryDbContext)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = deliveryDbContext;
    }

    public async Task<AddCurrencyResponse> Handle(AddCurrency request, CancellationToken cancellationToken)
    {
        var user = _httpContextAccessor.HttpContext?.User.Identities.FirstOrDefault().Name;
        if (user == null)
        {
            throw new UnauthorizedAccessException("User is not authenticated");
        }

        var newCurrency = new Currency
        {
            Name = request.Name,
            Shortcut = request.Shortcut,
            Created = DateTime.UtcNow,
            CreatedBy = user,
            ModifiedBy = user
        };

        _context.Currencies.Add(newCurrency);
        await _context.SaveChangesAsync(cancellationToken);

        return new AddCurrencyResponse();
    }
}
