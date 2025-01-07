using DeliveryApp.Domain.Entities;
using DeliveryApp.Persistance;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.Dictionaries.AddDictionaryType;

public class AddDictionaryTypeHandler : IRequestHandler<AddDictionaryType, AddDictionaryTypeResponse>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly DeliveryDbContext _context;
    public AddDictionaryTypeHandler(IHttpContextAccessor httpContextAccessor, DeliveryDbContext deliveryDbContext)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = deliveryDbContext;
    }

    public async Task<AddDictionaryTypeResponse> Handle(AddDictionaryType request, CancellationToken cancellationToken)
    {
        var existing = await _context.DictionaryTypes.FirstOrDefaultAsync(x => x.Name == request.Name);
        if (existing != null)
        {
            return new AddDictionaryTypeResponse("Dictionary type already exists");
        }

        var user = _httpContextAccessor.HttpContext?.User.Identities.FirstOrDefault().Name;
        if (user == null)
        {
            throw new UnauthorizedAccessException("User is not authenticated");
        }

        var newDictionaryType = new DictionaryType
        {
            Name = request.Name,
            Created = DateTime.UtcNow,
            CreatedBy = user ,
            ModifiedBy = user 
        };

        _context.DictionaryTypes.Add(newDictionaryType);
        await _context.SaveChangesAsync(cancellationToken);

        return new AddDictionaryTypeResponse();
    }
}
