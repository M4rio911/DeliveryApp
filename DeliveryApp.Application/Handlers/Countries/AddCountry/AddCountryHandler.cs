using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Persistance;

namespace DeliveryApp.Application.Handlers.Countries.AddCountry;

public class AddCountryHandler : ICommandHandler<AddCountry, AddCountryResponse>
{
    private readonly DeliveryDbContext _context;

    public AddCountryHandler(DeliveryDbContext deliveryDbContext)
    {
        _context = deliveryDbContext;
    }

    public async Task<AddCountryResponse> Handle(AddCountry request, CancellationToken cancellationToken)
    {
        var newCountry = new Country
        {
            Name = request.Name,
            Code = request.Code,
            CurrencyId = request.CurrencyId,
            Created = DateTime.UtcNow,
            CreatedBy = "testUser",
            ModifiedBy = "testUser"
        };

        _context.Countries.Add(newCountry);
        await _context.SaveChangesAsync(cancellationToken);

        return new AddCountryResponse();
    }
}
