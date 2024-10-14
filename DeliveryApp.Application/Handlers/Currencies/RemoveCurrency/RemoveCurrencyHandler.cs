using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Persistance;

namespace DeliveryApp.Application.Handlers.Currencies.RemoveCurrency;

public class RemoveCurrencyHandler : ICommandHandler<RemoveCurrency, RemoveCurrencyResponse>
{
    private readonly DeliveryDbContext _context;

    public RemoveCurrencyHandler(DeliveryDbContext deliveryDbContext)
    {
        _context = deliveryDbContext;
    }

    public async Task<RemoveCurrencyResponse> Handle(RemoveCurrency request, CancellationToken cancellationToken)
    {
        var carToRemove = _context.Currencies
            .FirstOrDefault(x => x.Id == request.CurrencyId);

        if (carToRemove == null)
        {
            return new RemoveCurrencyResponse("Currency with passed Id does not exists");
        }

        _context.Currencies.Remove(carToRemove);
        await _context.SaveChangesAsync();

        return new RemoveCurrencyResponse();
    }
}
