namespace DeliveryApp.Application.Handlers.Countries.AddCountry;

public class AddCountryParameters
{
    public string Name { get; set; }
    public string Code { get; set; }
    public int? CurrencyId { get; set; }
}