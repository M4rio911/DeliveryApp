using DeliveryApp.Application.Dto.Countries;
using DeliveryApp.Application.Handlers.BaseModel;
using DeliveryApp.Domain.Entities;

namespace DeliveryApp.Application.Handlers.Countries.EditCountry;

public class EditCountryResponse : BaseResponse
{
    public GetCountryDto Country { get; set; }
    public EditCountryResponse(Country car) : base(true, null) 
    {
        Country = new GetCountryDto
        {
            Name = car.Name,
            Code = car.Code,
            CurrencyId = car.CurrencyId
        };
    }
    public EditCountryResponse(string error) : base(error) { }
}
