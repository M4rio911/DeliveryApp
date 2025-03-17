using static DeliveryApp.Application.Interfaces.Mediator.ICommand;

namespace DeliveryApp.Application.Handlers.Transportations.AssignPackageToTransportation;

public class AssignPackageToTransportation : ICommand<AssignPackageToTransportationResponse>
{
    public int PackageId { get; set; }
    public int DriverId { get; set; }
    public DateTime TransportDate { get; set; }

    public AssignPackageToTransportation(AssignPackageToTransportationParameters parameters)
    {
        PackageId = parameters.PackageId;
        DriverId = parameters.DriverId;
        TransportDate = parameters.TransportDate;
    }
}