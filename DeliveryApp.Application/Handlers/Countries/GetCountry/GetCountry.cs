using DeliveryApp.Application.Interfaces.Mediator;

namespace DeliveryApp.Application.Handlers.Countries.GetCountry;

public class GetCountry : IQuery<GetCountryResponse>
{
    public int CountryId { get; set; }
    public GetCountry(GetCountryParameters parameters)
    {
        CountryId = parameters.CountryId;
    }
}