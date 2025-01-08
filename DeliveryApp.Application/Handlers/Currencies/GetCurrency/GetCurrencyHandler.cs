using DeliveryApp.Application.Dto.Currencies;
using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Persistance;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.Currencies.GetCurrency;

public class GetCurrencyHandler : IQueryHandler<GetCurrency, GetCurrencyResponse>
{
    private readonly DeliveryDbContext _context;

    public GetCurrencyHandler(DeliveryDbContext deliveryDbContext)
    {
        _context = deliveryDbContext;
    }

    public async Task<GetCurrencyResponse> Handle(GetCurrency request, CancellationToken cancellationToken)
    {
        var response = await _context.Currencies
            .Where(x => x.Id == request.CurrencyId)
            .Select(x => new GetCurrencyDto() 
            {
                Id = x.Id,
                Shortcut = x.Shortcut,
                Name = x.Name,
            }).FirstOrDefaultAsync(cancellationToken);

        if (response == null)
            return new GetCurrencyResponse("No Currency was found under passed Id");

        return new GetCurrencyResponse()
        {
            Currency = response
        };
    }
}
