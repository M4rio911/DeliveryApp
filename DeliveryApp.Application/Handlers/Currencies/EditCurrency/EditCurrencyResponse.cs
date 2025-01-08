using DeliveryApp.Application.Dto.Currencies;
using DeliveryApp.Application.Handlers.BaseModel;
using DeliveryApp.Domain.Entities;

namespace DeliveryApp.Application.Handlers.Currencies.EditCurrency;

public class EditCurrencyResponse : BaseResponse
{
    public GetCurrencyDto Currency { get; set; }
    public EditCurrencyResponse(Currency currency) : base(true, null) 
    {
        Currency = new GetCurrencyDto
        {
            Id = currency.Id,
            Name = currency.Name,
            Shortcut = currency.Shortcut
        };
    }
    public EditCurrencyResponse(string error) : base(error) { }
}
