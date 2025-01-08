using DeliveryApp.Application.Dto.Cars;
using DeliveryApp.Application.Dto.Currencies;
using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Persistance;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.Currencies.GetCurrencies;

public class GetCurrenciesHandler : IQueryHandler<GetCurrencies, GetCurrenciesResponse>
{
    private readonly DeliveryDbContext _context;

    public GetCurrenciesHandler(DeliveryDbContext deliveryDbContext)
    {
        _context = deliveryDbContext;
    }

    public async Task<GetCurrenciesResponse> Handle(GetCurrencies request, CancellationToken cancellationToken)
    {
        var response = await _context.Currencies
            .Select(x => new GetCurrencyDto() 
            {
                Id = x.Id,
                Shortcut = x.Shortcut,
                Name = x.Name
            })
            .OrderBy(x => x.Id)
            .ToListAsync();

        return new GetCurrenciesResponse()
        {
            Currencies = response
        };
    }
}
