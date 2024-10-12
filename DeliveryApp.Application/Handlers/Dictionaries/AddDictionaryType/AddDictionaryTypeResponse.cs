using DeliveryApp.Application.Handlers.BaseModel;

namespace DeliveryApp.Application.Handlers.Dictionaries.AddDictionaryType;

public class AddDictionaryTypeResponse : BaseResponse
{
    public AddDictionaryTypeResponse() : base(true, null) { }
    public AddDictionaryTypeResponse(string error) : base(error) { }
}
