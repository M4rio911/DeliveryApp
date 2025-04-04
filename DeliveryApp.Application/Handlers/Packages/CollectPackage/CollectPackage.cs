using static DeliveryApp.Application.Interfaces.Mediator.ICommand;

namespace DeliveryApp.Application.Handlers.Packages.CollectPackage;

public class CollectPackage : ICommand<CollectPackageResponse>
{
    public int PackageId { get; set; }
    public CollectPackage(CollectPackageParameters parameters)
    {
        PackageId = parameters.PackageId;
    }
}