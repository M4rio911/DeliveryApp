using static DeliveryApp.Application.Interfaces.Mediator.ICommand;

namespace DeliveryApp.Application.Handlers.Currencies.AddCurrency;

public class AddCurrency : ICommand<AddCurrencyResponse>
{
    public string Name { get; set; }
    public string Shortcut { get; set; }
    public AddCurrency(AddCurrencyParamenters parameters)
    {
        Name = parameters.Name;
        Shortcut = parameters.Shortcut;
    }
}
