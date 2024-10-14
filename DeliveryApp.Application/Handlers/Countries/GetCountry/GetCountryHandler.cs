using DeliveryApp.Application.Dto.Countries;
using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Persistance;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.Countries.GetCountry;

public class GetCountryHandler : IQueryHandler<GetCountry, GetCountryResponse>
{
    private readonly DeliveryDbContext _context;

    public GetCountryHandler(DeliveryDbContext deliveryDbContext)
    {
        _context = deliveryDbContext;
    }

    public async Task<GetCountryResponse> Handle(GetCountry request, CancellationToken cancellationToken)
    {
        var response = await _context.Countries
            .Where(x => x.Id == request.CountryId)
            .Select(x => new GetCountryDto() 
            {
                Name = x.Name,
                Code = x.Code,
                CurrencyId = x.CurrencyId
            }).FirstOrDefaultAsync(cancellationToken);

        if (response == null)
            return new GetCountryResponse("No Currency was found under passed Id");

        return new GetCountryResponse()
        {
            Country = response
        };
    }
}
