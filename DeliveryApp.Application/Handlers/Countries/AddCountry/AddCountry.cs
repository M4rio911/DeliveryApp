using static DeliveryApp.Application.Interfaces.Mediator.ICommand;

namespace DeliveryApp.Application.Handlers.Countries.AddCountry;

public class AddCountry : ICommand<AddCountryResponse>
{
    public string Name { get; set; }
    public string Code { get; set; }
    public int? CurrencyId { get; set; }

    public AddCountry(AddCountryParameters parameters)
    {
        Name = parameters.Name;
        Code = parameters.Code;
        CurrencyId = parameters.CurrencyId;
    }
}