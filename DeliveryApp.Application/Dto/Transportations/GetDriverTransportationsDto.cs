using DeliveryApp.Application.Dto.Packages;

namespace DeliveryApp.Application.Dto.Transportations;

public class GetDriverTransportationsDto
{
    public DateTime DateOfTransport {  get; set; }
    public int TransportationStatus { get; set; }
    public List<GetDriverTransportationPackageDto> PackagesToCollect { get; set; }
    public List<GetDriverTransportationPackageDto> PackagesToDelivery { get; set; }
    
}
