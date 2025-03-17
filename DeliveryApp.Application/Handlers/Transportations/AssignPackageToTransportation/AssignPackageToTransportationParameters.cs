using DeliveryApp.Domain.Entities;

namespace DeliveryApp.Application.Handlers.Transportations.AssignPackageToTransportation;

public class AssignPackageToTransportationParameters
{
    public int PackageId { get; set; }
    public int DriverId { get; set; }
    public DateTime TransportDate { get; set; }
}