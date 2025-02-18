using DeliveryApp.Application.Dto.Packages;
using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Persistance;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.Packages.GetPackages;

public class GetPackagesHandler : IQueryHandler<GetPackages, GetPackagesResponse>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly DeliveryDbContext _context;

    public GetPackagesHandler(IHttpContextAccessor httpContextAccessor, DeliveryDbContext deliveryDbContext)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = deliveryDbContext;
    }

    public async Task<GetPackagesResponse> Handle(GetPackages request, CancellationToken cancellationToken)
    {
        var response = await _context.Packages
            .Include(x => x.Sender)
            .Include(x => x.Reciver)
            .Select(x => new GetPackageDto() 
            {
                PackageId = x.Id,
                SenderEmail = x.Sender.Email,
                ReciverEmail = x.Reciver.Email,
                PackageStatusId = x.PackageStatusId,
                PackageTypeId = x.PackageTypeId,
                PaymentId = x.PaymentId,
            }).ToListAsync(cancellationToken);

        return new GetPackagesResponse()
        {
            Packages = response
        };
    }
}
