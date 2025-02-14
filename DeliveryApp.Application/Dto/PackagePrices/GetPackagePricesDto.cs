namespace DeliveryApp.Application.Dto.PackagePrices;

public class GetPackagePricesDto
{
    public int Id { get; set; }
    public int PackageTypeId { get; set; }
    public decimal Price { get; set; }
}
