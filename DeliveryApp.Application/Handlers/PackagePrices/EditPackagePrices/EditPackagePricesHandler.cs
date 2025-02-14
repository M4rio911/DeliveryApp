using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Persistance;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.PackagePrices.EditPackagePrices;

public class EditPackagePricesHandler : ICommandHandler<EditPackagePrices, EditPackagePricesResponse>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly DeliveryDbContext _context;

    public EditPackagePricesHandler(IHttpContextAccessor httpContextAccessor, DeliveryDbContext deliveryDbContext)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = deliveryDbContext;
    }

    public async Task<EditPackagePricesResponse> Handle(EditPackagePrices request, CancellationToken cancellationToken)
    {
        (bool valid, string errMessage) = ValidateRequest(request);
        if (!valid)
            return new EditPackagePricesResponse(errMessage);

        var user = _httpContextAccessor.HttpContext?.User.Identities.FirstOrDefault().Name;
        if (user == null)
        {
            throw new UnauthorizedAccessException("User is not authenticated");
        }

        foreach (var packagePrice in request.PackagePrices)
        {
            if(packagePrice == null) continue;

            if(packagePrice.Id == 0)
            {
                _context.PackagePrices.Add(new PackagePrice()
                {
                    PackageTypeId = packagePrice.PackageTypeId,
                    CurrencyId = request.CurrencyId,
                    Price = packagePrice.Price,
                    Created = DateTime.UtcNow,
                    CreatedBy = user
                });
                continue;
            }

            var dbPackagePrice = await _context.PackagePrices
                .Where(x => x.Id == packagePrice.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (dbPackagePrice == null)
                return new EditPackagePricesResponse("No PackagePrices was found under passed ID");

            dbPackagePrice.Price = packagePrice.Price;
            dbPackagePrice.Modified = DateTime.UtcNow;
            dbPackagePrice.ModifiedBy = user;
        }
        await _context.SaveChangesAsync(cancellationToken);

        return new EditPackagePricesResponse();
    }

    private static (bool, string) ValidateRequest(EditPackagePrices request)
    {
        if (request.CurrencyId == 0)
            return (false, "CurrencyId is null");

        if(request.PackagePrices.Any(x => x.PackageTypeId == 0))
            return (false, "PackagePrice data is invalid");

        return (true, "");
    }
}
