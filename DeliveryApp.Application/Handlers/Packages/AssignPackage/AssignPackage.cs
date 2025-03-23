using static DeliveryApp.Application.Interfaces.Mediator.ICommand;

namespace DeliveryApp.Application.Handlers.Packages.AssignPackage;

public class AssignPackage : ICommand<AssignPackageResponse>
{
    public int PackageId { get; set; }
    public int DriverId { get; set; }
    public DateTime TransportDate { get; set; }
    public AssignPackage(AssignPackageParameters parameters)
    {
        PackageId = parameters.PackageId;
        DriverId = parameters.DriverId;
        TransportDate = parameters.TransportDate;
    }
}