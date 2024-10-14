using static DeliveryApp.Application.Interfaces.Mediator.ICommand;

namespace DeliveryApp.Application.Handlers.Countries.EditCountry;

public class EditCountry : ICommand<EditCountryResponse>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public int? CurrencyId { get; set; }

    public EditCountry(EditCountryParameters parameters)
    {
        Id = parameters.Id;
        Name = parameters.Name;
        Code = parameters.Code;
        CurrencyId = parameters.CurrencyId;
    }
}