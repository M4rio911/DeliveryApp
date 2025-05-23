﻿using DeliveryApp.Application.Handlers.BaseModel;

namespace DeliveryApp.Application.Handlers.Dictionaries.RemoveDictionary;

public class RemoveDictionaryResponse : BaseResponse
{
    public RemoveDictionaryResponse() : base(true, null, true) { }
    public RemoveDictionaryResponse(string error) : base(error) { }
}
