using static DeliveryApp.Application.Interfaces.Mediator.ICommand;

namespace DeliveryApp.Application.Handlers.Packages.SendPackage;

public class SendPackage : ICommand<SendPackageResponse>
{
    public int PackageId { get; set; }
    public SendPackage(SendPackageParameters parameters)
    {
        PackageId = parameters.PackageId;
    }
}