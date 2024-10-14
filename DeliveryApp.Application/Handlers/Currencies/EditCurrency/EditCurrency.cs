using static DeliveryApp.Application.Interfaces.Mediator.ICommand;

namespace DeliveryApp.Application.Handlers.Currencies.EditCurrency;

public class EditCurrency : ICommand<EditCurrencyResponse>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Shortcut { get; set; }

    public EditCurrency(EditCurrencyParameters parameters)
    {
        Id = parameters.Id;
        Name = parameters.Name;
        Shortcut = parameters.Shortcut;
    }
}