using DeliveryApp.Application.Interfaces.Mediator;

namespace DeliveryApp.Application.Handlers.Packages.GetPackage;

public class GetPackage : IQuery<GetPackageResponse>
{
    public int PackageId { get; set; }
    public GetPackage(GetPackageParameters parameters)
    {
        PackageId = parameters.PackageId;
    }
}