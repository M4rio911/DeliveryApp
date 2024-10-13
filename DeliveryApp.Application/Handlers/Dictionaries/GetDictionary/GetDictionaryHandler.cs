using DeliveryApp.Application.Dto.Dictionaries;
using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Persistance;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.Dictionaries.GetDictionary;

public class GetDictionaryHandler : IQueryHandler<GetDictionary, GetDictionaryResponse>
{
    private readonly DeliveryDbContext _context;

    public GetDictionaryHandler(DeliveryDbContext deliveryDbContext)
    {
        _context = deliveryDbContext;
    }

    public async Task<GetDictionaryResponse> Handle(GetDictionary request, CancellationToken cancellationToken)
    {
        var response = await _context.Dictionaries
            .Where(x => x.Id == request.DictionaryId)
            .Select(x => new GetDictionaryDto() 
            {
                DictionaryTypeId = x.DictionaryTypeId,
                IsDefault = x.IsDefault,
                Name = x.Name
            }).FirstOrDefaultAsync(cancellationToken);

        if (response == null)
            return new GetDictionaryResponse("No Dictionary was found under passed Id");

        return new GetDictionaryResponse()
        {
            Dictionary = response
        };
    }
}
