using static DeliveryApp.Application.Interfaces.Mediator.ICommand;

namespace DeliveryApp.Application.Handlers.Currencies.RemoveCurrency;

public class RemoveCurrency : ICommand<RemoveCurrencyResponse>
{
    public int CurrencyId { get; set; }

    public RemoveCurrency(RemoveCurrencyParameters parameters)
    {
        CurrencyId = parameters.CurrencyId;
    }
}