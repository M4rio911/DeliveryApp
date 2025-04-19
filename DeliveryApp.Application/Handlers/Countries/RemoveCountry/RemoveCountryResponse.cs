using DeliveryApp.Application.Handlers.BaseModel;

namespace DeliveryApp.Application.Handlers.Countries.RemoveCountry;

public class RemoveCountryResponse : BaseResponse
{
    public RemoveCountryResponse() : base(true, null, true) { }
    public RemoveCountryResponse(string error) : base(error) { }
}
