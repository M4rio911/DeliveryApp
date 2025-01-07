using DeliveryApp.Domain.Entities;
using DeliveryApp.Persistance;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.Dictionaries.AddDictionary;

public class AddDictionaryHandler : IRequestHandler<AddDictionary, AddDictionaryResponse>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly DeliveryDbContext _context;
    public AddDictionaryHandler(IHttpContextAccessor httpContextAccessor, DeliveryDbContext deliveryDbContext)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = deliveryDbContext;
    }

    public async Task<AddDictionaryResponse> Handle(AddDictionary request, CancellationToken cancellationToken)
    {
        var existing = await _context.Dictionaries
            .FirstOrDefaultAsync(x => x.Name == request.Name && x.DictionaryTypeId == request.DictionaryTypeId, cancellationToken);

        if (existing != null)
        {
            return new AddDictionaryResponse("Dictionary already exists");
        }

        var existingDefaultDictionary = await _context.Dictionaries
                .FirstOrDefaultAsync(x => x.IsDefault);

        if (existingDefaultDictionary == null)
        {
            request.IsDefault = true;
        }
        else if ( request.IsDefault)
        {
            return new AddDictionaryResponse("Default dictionary in this type already exists");
        }

        var user = _httpContextAccessor.HttpContext?.User.Identities.FirstOrDefault().Name;
        if (user == null)
        {
            throw new UnauthorizedAccessException("User is not authenticated");
        }

        var newDictionary = new Dictionary
        {
            Name = request.Name,
            DictionaryTypeId = request.DictionaryTypeId,
            IsDefault = request.IsDefault,
            Created = DateTime.UtcNow,
            Modified = DateTime.UtcNow,
            CreatedBy = user,
            ModifiedBy = user
        };

        _context.Dictionaries.Add(newDictionary);
        await _context.SaveChangesAsync(cancellationToken);

        return new AddDictionaryResponse();
    }
}
