using DeliveryApp.Application.Dto.Dictionaries;
using DeliveryApp.Application.Handlers.BaseModel;
using DeliveryApp.Domain.Entities;

namespace DeliveryApp.Application.Handlers.Dictionaries.EditDictionaryType;

public class EditDictionaryTypeResponse : BaseResponse
{
    public GetDictionaryTypeDto DictionaryType { get; set; }
    public EditDictionaryTypeResponse(DictionaryType dictionaryType) : base(true, null, true)
    {
        DictionaryType = new GetDictionaryTypeDto()
        {
            DictionaryTypeId = dictionaryType.Id,
            Name = dictionaryType.Name
        };
    }
    public EditDictionaryTypeResponse(string error) : base(error) { }
}
