namespace DeliveryApp.Application.Dto.Packages;

public class GetPackageDto
{
    public int PackageId { get; set; }
    public string SenderEmail { get; set; }
    public string ReciverEmail { get; set; }
    public int PackageTypeId { get; set; }
    public int PackageStatusId { get; set; }
    public int PaymentId { get; set; }
    public DateTime? ArrivalDate { get; set; }
}
