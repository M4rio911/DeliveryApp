using DeliveryApp.Application.Dto.PackagePrices;
using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Persistance;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.PackagePrices.GetPackagePrices;

public class GetPackagePricesHandler : IQueryHandler<GetPackagePrices, GetPackagePricesResponse>
{
    private readonly DeliveryDbContext _context;

    public GetPackagePricesHandler(DeliveryDbContext deliveryDbContext)
    {
        _context = deliveryDbContext;
    }

    public async Task<GetPackagePricesResponse> Handle(GetPackagePrices request, CancellationToken cancellationToken)
    {
        var response = await _context.PackagePrices
            .Where(x => x.CurrencyId == request.CurrencyId)
            .Select(x => new GetPackagePricesDto() 
            {
                Id = x.Id,
                PackageTypeId = x.PackageTypeId,
                Price = x.Price
            })
            .OrderBy(x => x.Id)
            .ToListAsync(cancellationToken);

        return new GetPackagePricesResponse()
        {
            PackagePrices = response
        };
    }
}
