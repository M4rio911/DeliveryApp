using DeliveryApp.Application.Dto.Dictionaries;
using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Persistance;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.Dictionaries.GetDictionaryType;

public class GetDictionaryTypeHandler : IQueryHandler<GetDictionaryType, GetDictionaryTypeResponse>
{
    private readonly DeliveryDbContext _context;

    public GetDictionaryTypeHandler(DeliveryDbContext deliveryDbContext)
    {
        _context = deliveryDbContext;
    }

    public async Task<GetDictionaryTypeResponse> Handle(GetDictionaryType request, CancellationToken cancellationToken)
    {
        var response = await _context.DictionaryTypes
            .Where(x => x.Id == request.DictionaryTypeId)
            .Select(x => new GetDictionaryTypeDto() 
            {
                Name = x.Name
            }).FirstOrDefaultAsync(cancellationToken);

        if (response == null)
            return new GetDictionaryTypeResponse("No DictionaryType was found under passed Id");

        return new GetDictionaryTypeResponse()
        {
            DictionaryType = response
        };
    }
}
