namespace DeliveryApp.Application.Handlers.Countries.EditCountry;

public class EditCountryParameters
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public int? CurrencyId { get; set; }
}