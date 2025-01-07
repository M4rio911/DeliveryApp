using DeliveryApp.Application.Dto.Dictionaries;
using DeliveryApp.Application.Handlers.BaseModel;

namespace DeliveryApp.Application.Handlers.Dictionaries.GetDictionariesByType;

public class GetDictionariesByTypeResponse : BaseResponse
{
    public IList<GetDictionaryDto> Dictionaries { get; set; }

    public GetDictionariesByTypeResponse() : base(true, null) { }
    public GetDictionariesByTypeResponse(string error) : base(error) { }
}
