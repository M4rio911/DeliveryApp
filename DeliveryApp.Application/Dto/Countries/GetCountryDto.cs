namespace DeliveryApp.Application.Dto.Countries;

public class GetCountryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public int? CurrencyId { get; set; }
}
