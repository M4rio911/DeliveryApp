using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Persistance;

namespace DeliveryApp.Application.Handlers.Currencies.AddCurrency;

public class AddCurrencyHandler : ICommandHandler<AddCurrency, AddCurrencyResponse>
{
    private readonly DeliveryDbContext _context;

    public AddCurrencyHandler(DeliveryDbContext deliveryDbContext)
    {
        _context = deliveryDbContext;
    }

    public async Task<AddCurrencyResponse> Handle(AddCurrency request, CancellationToken cancellationToken)
    {
        var newCurrency = new Currency
        {
            Name = request.Name,
            Shortcut = request.Shortcut,
            Created = DateTime.UtcNow,
            CreatedBy = "testUser",
            ModifiedBy = "testUser"
        };

        _context.Currencies.Add(newCurrency);
        await _context.SaveChangesAsync(cancellationToken);

        return new AddCurrencyResponse();
    }
}
