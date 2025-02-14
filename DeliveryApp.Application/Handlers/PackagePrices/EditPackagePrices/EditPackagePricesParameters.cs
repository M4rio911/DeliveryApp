using DeliveryApp.Application.Dto.PackagePrices;

namespace DeliveryApp.Application.Handlers.PackagePrices.EditPackagePrices;

public class EditPackagePricesParameters
{
    public int CurrencyId { get; set; }
    public List<GetPackagePricesDto> PackagePrices { get; set; }
}