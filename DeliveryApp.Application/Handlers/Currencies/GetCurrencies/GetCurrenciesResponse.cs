using DeliveryApp.Application.Dto.Currencies;
using DeliveryApp.Application.Handlers.BaseModel;

namespace DeliveryApp.Application.Handlers.Currencies.GetCurrencies;

public class GetCurrenciesResponse : BaseResponse
{
    public List<GetCurrencyDto> Currencies { get; set; }

    public GetCurrenciesResponse() : base(true, null) { }
    public GetCurrenciesResponse(List<string> errors) : base(errors) { }
}
