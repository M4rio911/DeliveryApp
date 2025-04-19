using DeliveryApp.Application.Handlers.BaseModel;

namespace DeliveryApp.Application.Handlers.Currencies.AddCurrency;

public class AddCurrencyResponse : BaseResponse
{
    public AddCurrencyResponse() : base(true, null, true) { }
    public AddCurrencyResponse(string error) : base(error) { }
}
