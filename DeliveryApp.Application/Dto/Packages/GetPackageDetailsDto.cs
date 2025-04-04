using DeliveryApp.Application.Dto.Addresses;
using DeliveryApp.Application.Dto.Payments;

namespace DeliveryApp.Application.Dto.Packages;

public class GetDriverTransportationPackageDto
{
    public int PackageId { get; set; }
    public string SenderEmail { get; set; }
    public string ReciverEmail { get; set; }
    public int PackageTypeId { get; set; }
    public int PackageStatusId { get; set; }
    public GetPaymentDto Payment { get; set; }
    public GetAddressDto DestinationAddress { get; set; }
}
