namespace DeliveryApp.Application.Handlers.Packages.AssignPackage;

public class AssignPackageParameters
{
    public int PackageId { get; set; }
    public int DriverId { get; set; }
    public DateTime TransportDate { get; set; }
}
