namespace DeliveryApp.Application.Dto.Transportations;

public class GetDriverTransportationPackageDto
{
    public int PackageId { get; set; }
    public string SenderEmail { get; set; }
    public string ReciverEmail { get; set; }
    public int PackageTypeId { get; set; }
    public int PackageStatusId { get; set; }
    //ADDRESS
    public string Country { get; set; }
    public string PostCode { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string Number { get; set; }
    public int AddressTypeId { get; set; }
}
