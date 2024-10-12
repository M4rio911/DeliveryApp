using DeliveryApp.Domain.Entities;
using DeliveryApp.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.Dictionaries.AddDictionaryType;

public class AddDictionaryTypeHandler : IRequestHandler<AddDictionaryType, AddDictionaryTypeResponse>
{
    private readonly DeliveryDbContext _context;
    public AddDictionaryTypeHandler(DeliveryDbContext deliveryDbContext)
    {
        _context = deliveryDbContext;
    }

    public async Task<AddDictionaryTypeResponse> Handle(AddDictionaryType request, CancellationToken cancellationToken)
    {
        var existing = await _context.DictionaryTypes.FirstOrDefaultAsync(x => x.Name == request.Name);

        if (existing != null)
        {
            return new AddDictionaryTypeResponse("Dictionary type already exists");
        }

        var newDictionaryType = new DictionaryType
        {
            Name = request.Name,
            Created = DateTime.UtcNow,
            CreatedBy = "testUser",
            ModifiedBy = "testUser"
        };

        _context.DictionaryTypes.Add(newDictionaryType);
        await _context.SaveChangesAsync(cancellationToken);

        return new AddDictionaryTypeResponse();
    }
}
