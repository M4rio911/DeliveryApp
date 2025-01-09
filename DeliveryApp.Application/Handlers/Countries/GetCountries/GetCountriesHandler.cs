using DeliveryApp.Application.Dto.Countries;
using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Persistance;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.Countries.GetCountries;

public class GetCountriesHandler : IQueryHandler<GetCountries, GetCountriesResponse>
{
    private readonly DeliveryDbContext _context;

    public GetCountriesHandler(DeliveryDbContext deliveryDbContext)
    {
        _context = deliveryDbContext;
    }

    public async Task<GetCountriesResponse> Handle(GetCountries request, CancellationToken cancellationToken)
    {
        var response = await _context.Countries
            .Select(x => new GetCountryDto() 
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                CurrencyId = x.CurrencyId
            }).ToListAsync();

        return new GetCountriesResponse()
        {
            Countries = response
        };
    }
}
