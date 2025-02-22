using DeliveryApp.Application.Dto.Packages;
using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Persistance;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.Packages.GetPackage;

public class GetPackageHandler : IQueryHandler<GetPackage, GetPackageResponse>
{
    private readonly DeliveryDbContext _context;

    public GetPackageHandler(DeliveryDbContext deliveryDbContext)
    {
        _context = deliveryDbContext;
    }

    public async Task<GetPackageResponse> Handle(GetPackage request, CancellationToken cancellationToken)
    {
        var response = await _context.Packages
            .Where(x => x.Id == request.PackageId)
            .Select(x => new GetPackageDto() 
            {
                PackageId = x.Id,
                SenderEmail = x.Sender.Email,
                ReciverEmail = x.Reciver.Email,
                PackageStatusId = x.PackageStatusId,
                PackageTypeId = x.PackageTypeId,
                PaymentId = x.PaymentId,
            }).FirstOrDefaultAsync(cancellationToken);

        if (response == null)
            return new GetPackageResponse("No package was found under passed Id");

        return new GetPackageResponse()
        {
            Package = response
        };
    }
}
