using DeliveryApp.Persistance;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.Dictionaries.EditDictionaryType;

public class EditDictionaryTypeHandler : IRequestHandler<EditDictionaryType, EditDictionaryTypeResponse>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly DeliveryDbContext _context;
    public EditDictionaryTypeHandler(IHttpContextAccessor httpContextAccessor, DeliveryDbContext deliveryDbContext)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = deliveryDbContext;
    }

    public async Task<EditDictionaryTypeResponse> Handle(EditDictionaryType request, CancellationToken cancellationToken)
    {
        var dbDictionaryType = await _context.DictionaryTypes
            .FirstOrDefaultAsync(x => 
            x.Id == request.DictionaryTypeId, cancellationToken);

        if (dbDictionaryType == null)
            return new EditDictionaryTypeResponse("No car dictionary type found under passed ID");

        var user = _httpContextAccessor.HttpContext?.User.Identities.FirstOrDefault().Name;
        if (user == null)
        {
            throw new UnauthorizedAccessException("User is not authenticated");
        }

        dbDictionaryType.Name = request.Name;
        dbDictionaryType.Modified = DateTime.UtcNow;
        dbDictionaryType.ModifiedBy = user;

        await _context.SaveChangesAsync(cancellationToken);

        return new EditDictionaryTypeResponse(dbDictionaryType);
    }
}
