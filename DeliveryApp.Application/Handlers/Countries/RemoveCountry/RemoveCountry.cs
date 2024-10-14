using static DeliveryApp.Application.Interfaces.Mediator.ICommand;

namespace DeliveryApp.Application.Handlers.Countries.RemoveCountry;

public class RemoveCountry : ICommand<RemoveCountryResponse>
{
    public int CountryId { get; set; }

    public RemoveCountry(RemoveCountryParameters parameters)
    {
        CountryId = parameters.CountryId;
    }
}