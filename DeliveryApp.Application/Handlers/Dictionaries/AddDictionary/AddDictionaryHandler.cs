using DeliveryApp.Domain.Entities;
using DeliveryApp.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.Dictionaries.AddDictionary;

public class AddDictionaryHandler : IRequestHandler<AddDictionary, AddDictionaryResponse>
{
    private readonly DeliveryDbContext _context;
    public AddDictionaryHandler(DeliveryDbContext deliveryDbContext)
    {
        _context = deliveryDbContext;
    }

    public async Task<AddDictionaryResponse> Handle(AddDictionary request, CancellationToken cancellationToken)
    {
        var existing = await _context.Dictionaries
            //.FirstOrDefaultAsync(x => x.Name == request.Name && x.DictionaryTypeId == request.DictionaryTypeId, cancellationToken);
            .FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);

        if (existing != null)
        {
            return new AddDictionaryResponse("Dictionary already exists");
        }

        if(request.IsDefault)
        {
            var existingDefaultDictionary = await _context.Dictionaries
                .FirstOrDefaultAsync(x => x.IsDefault);

            if(existingDefaultDictionary != null) 
                return new AddDictionaryResponse("Default dictionary in this type already exists");
        }

        var newDictionary = new Dictionary
        {
            Name = request.Name,
            DictionaryTypeId = request.DictionaryTypeId,
            IsDefault = request.IsDefault,
            Created = DateTime.UtcNow,
            CreatedBy = "testUser",
            ModifiedBy = "testUser"
        };

        _context.Dictionaries.Add(newDictionary);
        await _context.SaveChangesAsync(cancellationToken);

        return new AddDictionaryResponse();
    }
}
