﻿using DeliveryApp.Application.Handlers.BaseModel;

namespace DeliveryApp.Application.Handlers.Dictionaries.AddDictionary;

public class AddDictionaryResponse : BaseResponse
{
    public AddDictionaryResponse() : base(true, null) { }
    public AddDictionaryResponse(string error) : base(error) { }
}
