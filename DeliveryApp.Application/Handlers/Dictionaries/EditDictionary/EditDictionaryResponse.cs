using DeliveryApp.Application.Dto.Dictionaries;
using DeliveryApp.Application.Handlers.BaseModel;
using DeliveryApp.Domain.Entities;

namespace DeliveryApp.Application.Handlers.Dictionaries.EditDictionary;

public class EditDictionaryResponse : BaseResponse
{
    public GetDictionaryDto Dictionary { get; set; }
    public EditDictionaryResponse(Dictionary dictionary) : base(true, null)
    {
        Dictionary = new GetDictionaryDto()
        {
            DictionaryId = dictionary.Id,
            DictionaryTypeId = dictionary.DictionaryTypeId,
            IsDefault = dictionary.IsDefault,
            Name = dictionary.Name
        };
    }
    public EditDictionaryResponse(string error) : base(error) { }
}
