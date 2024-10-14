using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Persistance;

namespace DeliveryApp.Application.Handlers.Countries.RemoveCountry;

public class RemoveCountryHandler : ICommandHandler<RemoveCountry, RemoveCountryResponse>
{
    private readonly DeliveryDbContext _context;

    public RemoveCountryHandler(DeliveryDbContext deliveryDbContext)
    {
        _context = deliveryDbContext;
    }

    public async Task<RemoveCountryResponse> Handle(RemoveCountry request, CancellationToken cancellationToken)
    {
        var carToRemove = _context.Countries
            .FirstOrDefault(x => x.Id == request.CountryId);

        if (carToRemove == null)
        {
            return new RemoveCountryResponse("Country with passed Id does not exists");
        }

        _context.Countries.Remove(carToRemove);
        await _context.SaveChangesAsync(cancellationToken);

        return new RemoveCountryResponse();
    }
}
