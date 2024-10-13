using DeliveryApp.Application.Dto.Dictionaries;
using DeliveryApp.Application.Handlers.BaseModel;

namespace DeliveryApp.Application.Handlers.Dictionaries.GetDictionaryType;

public class GetDictionaryTypeResponse : BaseResponse
{
    public GetDictionaryTypeDto DictionaryType { get; set; }

    public GetDictionaryTypeResponse() : base(true, null) { }
    public GetDictionaryTypeResponse(string error) : base(error) { }
}
