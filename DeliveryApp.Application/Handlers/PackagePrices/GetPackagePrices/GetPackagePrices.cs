using DeliveryApp.Application.Interfaces.Mediator;

namespace DeliveryApp.Application.Handlers.PackagePrices.GetPackagePrices;

public class GetPackagePrices : IQuery<GetPackagePricesResponse>
{
    public int CurrencyId { get; set; }
    public GetPackagePrices(int currencyId)
    {
        CurrencyId = currencyId;
    }
}