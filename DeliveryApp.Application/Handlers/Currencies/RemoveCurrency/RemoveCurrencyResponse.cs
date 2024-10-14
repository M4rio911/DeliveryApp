using DeliveryApp.Application.Handlers.BaseModel;

namespace DeliveryApp.Application.Handlers.Currencies.RemoveCurrency;

public class RemoveCurrencyResponse : BaseResponse
{
    public RemoveCurrencyResponse() : base(true, null) { }
    public RemoveCurrencyResponse(string error) : base(error) { }
}
