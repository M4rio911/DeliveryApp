using DeliveryApp.Application.Handlers.BaseModel;

namespace DeliveryApp.Application.Handlers.Countries.AddCountry;

public class AddCountryResponse : BaseResponse
{
    public AddCountryResponse() : base(true, null) { }
    public AddCountryResponse(List<string> errors) : base(errors) { }
}
