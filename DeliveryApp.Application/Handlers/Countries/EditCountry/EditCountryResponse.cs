using DeliveryApp.Application.Dto.Countries;
using DeliveryApp.Application.Handlers.BaseModel;
using DeliveryApp.Domain.Entities;

namespace DeliveryApp.Application.Handlers.Countries.EditCountry;

public class EditCountryResponse : BaseResponse
{
    public GetCountryDto Country { get; set; }
    public EditCountryResponse(Country country) : base(true, null) 
    {
        Country = new GetCountryDto
        {
            Id = country.Id,
            Name = country.Name,
            Code = country.Code,
            CurrencyId = country.CurrencyId
        };
    }
    public EditCountryResponse(string error) : base(error) { }
}
