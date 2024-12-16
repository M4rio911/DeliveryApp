using DeliveryApp.Application.Dto.Cars;
using DeliveryApp.Application.Dto.Dictionaries;
using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Persistance;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.Dictionaries.GetDictionaryTypes;

public class GetDictionaryTypesHandler : IQueryHandler<GetDictionaryTypes, GetDictionaryTypesResponse>
{
    private readonly DeliveryDbContext _context;

    public GetDictionaryTypesHandler(DeliveryDbContext deliveryDbContext)
    {
        _context = deliveryDbContext;
    }

    public async Task<GetDictionaryTypesResponse> Handle(GetDictionaryTypes request, CancellationToken cancellationToken)
    {
        var response = await _context.DictionaryTypes
            .Select(x => new GetDictionaryTypesDto() 
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync(cancellationToken);

        if (response == null)
            return new GetDictionaryTypesResponse("No DictionaryTypes was found");

        return new GetDictionaryTypesResponse()
        {
            DictionaryTypes = response
        };
    }
}
