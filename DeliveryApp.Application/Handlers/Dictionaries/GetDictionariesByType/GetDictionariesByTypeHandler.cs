using DeliveryApp.Application.Dto.Dictionaries;
using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Persistance;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.Dictionaries.GetDictionariesByType;

public class GetDictionariesByTypeHandler : IQueryHandler<GetDictionariesByType, GetDictionariesByTypeResponse>
{
    private readonly DeliveryDbContext _context;

    public GetDictionariesByTypeHandler(DeliveryDbContext deliveryDbContext)
    {
        _context = deliveryDbContext;
    }

    public async Task<GetDictionariesByTypeResponse> Handle(GetDictionariesByType request, CancellationToken cancellationToken)
    {
        var response = await _context.Dictionaries
            .Where(x => x.DictionaryTypeId == request.DictionaryTypeId)
            .Select(x => new GetDictionaryDto()
            {
                DictionaryTypeId = x.Id,
                DictionaryId = x.Id,
                IsDefault = x.IsDefault,
                Name = x.Name
            })
            .ToListAsync(cancellationToken);

        if (response == null)
            return new GetDictionariesByTypeResponse("No DictionaryTypes was found");

        return new GetDictionariesByTypeResponse()
        {
            Dictionaries = response
        };
    }
}
