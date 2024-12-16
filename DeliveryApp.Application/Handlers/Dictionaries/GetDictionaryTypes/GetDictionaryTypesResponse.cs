using DeliveryApp.Application.Dto.Dictionaries;
using DeliveryApp.Application.Handlers.BaseModel;

namespace DeliveryApp.Application.Handlers.Dictionaries.GetDictionaryTypes;

public class GetDictionaryTypesResponse : BaseResponse
{
    public IList<GetDictionaryTypesDto> DictionaryTypes { get; set; }

    public GetDictionaryTypesResponse() : base(true, null) { }
    public GetDictionaryTypesResponse(string error) : base(error) { }
}
