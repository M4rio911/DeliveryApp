using DeliveryApp.Application.Dto.Countries;
using DeliveryApp.Application.Handlers.BaseModel;

namespace DeliveryApp.Application.Handlers.Countries.GetCountries;

public class GetCountriesResponse : BaseResponse
{
    public List<GetCountryDto> Countries { get; set; }

    public GetCountriesResponse() : base(true, null) { }
    public GetCountriesResponse(List<string> errors) : base(errors) { }
}
