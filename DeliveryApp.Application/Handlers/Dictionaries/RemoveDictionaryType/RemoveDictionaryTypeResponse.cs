using DeliveryApp.Application.Handlers.BaseModel;

namespace DeliveryApp.Application.Handlers.Dictionaries.RemoveDictionaryType;

public class RemoveDictionaryTypeResponse : BaseResponse
{
    public RemoveDictionaryTypeResponse() : base(true, null) { }
    public RemoveDictionaryTypeResponse(string error) : base(error) { }
}
