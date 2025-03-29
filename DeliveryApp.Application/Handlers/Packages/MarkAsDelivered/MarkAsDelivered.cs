using static DeliveryApp.Application.Interfaces.Mediator.ICommand;

namespace DeliveryApp.Application.Handlers.Packages.MarkAsDelivered;

public class MarkAsDelivered : ICommand<MarkAsDeliveredResponse>
{
    public int PackageId { get; set; }
    public MarkAsDelivered(MarkAsDeliveredParameters parameters)
    {
        PackageId = parameters.PackageId;
    }
}