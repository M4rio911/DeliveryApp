using DeliveryApp.Domain.Entities;
using DeliveryApp.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.Dictionaries.RemoveDictionary;

public class RemoveDictionaryHandler : IRequestHandler<RemoveDictionary, RemoveDictionaryResponse>
{
    private readonly DeliveryDbContext _context;
    public RemoveDictionaryHandler(DeliveryDbContext deliveryDbContext)
    {
        _context = deliveryDbContext;
    }

    public async Task<RemoveDictionaryResponse> Handle(RemoveDictionary request, CancellationToken cancellationToken)
    {
        var dictionaryToRemove = _context.Dictionaries
            .FirstOrDefault(x => 
                x.Id == request.DictionaryId && 
                x.DictionaryTypeId == request.DictionaryTypeId);

        if (dictionaryToRemove == null)
        {
            return new RemoveDictionaryResponse("Dictionary with passed Id does not exists");
        }

        _context.Dictionaries.Remove(dictionaryToRemove);
        await _context.SaveChangesAsync(cancellationToken);

        return new RemoveDictionaryResponse();
    }
}
