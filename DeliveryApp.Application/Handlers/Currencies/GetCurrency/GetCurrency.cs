using DeliveryApp.Application.Interfaces.Mediator;

namespace DeliveryApp.Application.Handlers.Currencies.GetCurrency;

public class GetCurrency : IQuery<GetCurrencyResponse>
{
    public int CurrencyId { get; set; }
    public GetCurrency(GetCurrencyParameters parameters)
    {
        CurrencyId = parameters.CurrencyId;
    }
}