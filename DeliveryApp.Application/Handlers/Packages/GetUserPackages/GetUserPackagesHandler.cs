using DeliveryApp.Application.Dto.Packages;
using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Persistance;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DeliveryApp.Application.Handlers.Packages.GetUserPackages;

public class GetUserPackagesHandler : IQueryHandler<GetUserPackages, GetUserPackagesResponse>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly DeliveryDbContext _context;

    public GetUserPackagesHandler(IHttpContextAccessor httpContextAccessor, DeliveryDbContext deliveryDbContext)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = deliveryDbContext;
    }

    public async Task<GetUserPackagesResponse> Handle(GetUserPackages request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            throw new UnauthorizedAccessException("User is not authenticated");
        }

        var response = await _context.Packages
            .Include(x => x.Sender)
            .Include(x => x.Reciver)
            .Where(x => x.SenderId == userId)
            .Select(x => new GetPackageDto() 
            {
                PackageId = x.Id,
                SenderEmail = x.Sender.Email,
                ReciverEmail = x.Reciver.Email,
                PackageStatusId = x.PackageStatusId,
                PackageTypeId = x.PackageTypeId,
            }).ToListAsync(cancellationToken);

        return new GetUserPackagesResponse()
        {
            UserPackages = response
        };
    }
}
