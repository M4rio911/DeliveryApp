using DeliveryApp.Application.Dto.Countries;
using DeliveryApp.Application.Handlers.BaseModel;

namespace DeliveryApp.Application.Handlers.Countries.GetCountry;

public class GetCountryResponse : BaseResponse
{
    public GetCountryDto Country { get; set; }

    public GetCountryResponse() : base(true, null) { }
    public GetCountryResponse(string error) : base(error) { }
}
