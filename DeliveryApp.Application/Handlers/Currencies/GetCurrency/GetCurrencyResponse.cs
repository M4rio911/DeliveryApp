using DeliveryApp.Application.Dto.Cars;
using DeliveryApp.Application.Dto.Currencies;
using DeliveryApp.Application.Handlers.BaseModel;

namespace DeliveryApp.Application.Handlers.Currencies.GetCurrency;

public class GetCurrencyResponse : BaseResponse
{
    public GetCurrencyDto Currrency { get; set; }

    public GetCurrencyResponse() : base(true, null) { }
    public GetCurrencyResponse(string error) : base(error) { }
}
