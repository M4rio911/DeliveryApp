using DeliveryApp.Domain.Entities;
using DeliveryApp.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.Dictionaries.RemoveDictionaryType;

public class RemoveDictionaryTypeHandler : IRequestHandler<RemoveDictionaryType, RemoveDictionaryTypeResponse>
{
    private readonly DeliveryDbContext _context;
    public RemoveDictionaryTypeHandler(DeliveryDbContext deliveryDbContext)
    {
        _context = deliveryDbContext;
    }

    public async Task<RemoveDictionaryTypeResponse> Handle(RemoveDictionaryType request, CancellationToken cancellationToken)
    {
        var dictionaryTypeToRemove = _context.DictionaryTypes
            .FirstOrDefault(x => x.Id == request.Id);

        if (dictionaryTypeToRemove == null)
        {
            return new RemoveDictionaryTypeResponse("DictionaryType with passed Id does not exists");
        }

        _context.DictionaryTypes.Remove(dictionaryTypeToRemove);
        await _context.SaveChangesAsync(cancellationToken);

        return new RemoveDictionaryTypeResponse();
    }
}
