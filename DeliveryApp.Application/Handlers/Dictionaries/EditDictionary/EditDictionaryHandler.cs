using DeliveryApp.Application.Handlers.Dictionaries.AddDictionary;
using DeliveryApp.Persistance;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.Dictionaries.EditDictionary;

public class EditDictionaryHandler : IRequestHandler<EditDictionary, EditDictionaryResponse>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly DeliveryDbContext _context;
    public EditDictionaryHandler(IHttpContextAccessor httpContextAccessor, DeliveryDbContext deliveryDbContext)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = deliveryDbContext;
    }

    public async Task<EditDictionaryResponse> Handle(EditDictionary request, CancellationToken cancellationToken)
    {
        var user = _httpContextAccessor.HttpContext?.User.Identities.FirstOrDefault().Name;
        if (user == null)
        {
            throw new UnauthorizedAccessException("User is not authenticated");
        }

        var dbDictionary = await _context.Dictionaries
            .FirstOrDefaultAsync(x =>
            x.Id == request.DictionaryId &&
            x.DictionaryTypeId == request.DictionaryTypeId, cancellationToken);

        if (dbDictionary == null)
            return new EditDictionaryResponse("No dictionary was found under passed ID");

        if (request.IsDefault)
        {
            var existingDefaultDictionary = await _context.Dictionaries
                .FirstOrDefaultAsync(x => x.IsDefault && x.DictionaryTypeId == request.DictionaryTypeId);

            if (existingDefaultDictionary != null &&
                existingDefaultDictionary.Id != request.DictionaryId)
                return new EditDictionaryResponse("Default dictionary in this type already exists");
        }

        dbDictionary.Name = request.Name;
        dbDictionary.IsDefault = request.IsDefault;
        dbDictionary.Modified = DateTime.UtcNow;
        dbDictionary.ModifiedBy = user;

        await _context.SaveChangesAsync(cancellationToken);

        return new EditDictionaryResponse(dbDictionary);
    }
}
