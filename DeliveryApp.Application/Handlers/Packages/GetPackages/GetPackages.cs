using DeliveryApp.Application.Interfaces.Mediator;

namespace DeliveryApp.Application.Handlers.Packages.GetPackages;

public class GetPackages : IQuery<GetPackagesResponse>
{
    public int PackageStatusId { get; set; }
    public GetPackages(GetPackagesParameters parameters)
    {
        PackageStatusId = parameters.PackageStatusId ?? 0;
    }
}