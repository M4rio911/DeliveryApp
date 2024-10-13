using DeliveryApp.Application.Dto.Dictionaries;
using DeliveryApp.Application.Handlers.BaseModel;

namespace DeliveryApp.Application.Handlers.Dictionaries.GetDictionary;

public class GetDictionaryResponse : BaseResponse
{
    public GetDictionaryDto Dictionary { get; set; }

    public GetDictionaryResponse() : base(true, null) { }
    public GetDictionaryResponse(string error) : base(error) { }
}
