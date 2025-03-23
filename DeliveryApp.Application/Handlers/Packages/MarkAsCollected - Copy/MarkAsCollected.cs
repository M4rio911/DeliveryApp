using static DeliveryApp.Application.Interfaces.Mediator.ICommand;

namespace DeliveryApp.Application.Handlers.Packages.MarkAsCollected;

public class MarkAsCollected : ICommand<MarkAsCollectedResponse>
{
    public int PackageId { get; set; }
    public MarkAsCollected(MarkAsCollectedParameters parameters)
    {
        PackageId = parameters.PackageId;
    }
}