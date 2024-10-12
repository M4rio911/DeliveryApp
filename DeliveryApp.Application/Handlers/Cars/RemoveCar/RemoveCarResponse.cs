﻿using DeliveryApp.Application.Handlers.BaseModel;

namespace DeliveryApp.Application.Handlers.Cars.RemoveCar;

public class RemoveCarResponse : BaseResponse
{
    public RemoveCarResponse() : base(true, null) { }
    public RemoveCarResponse(List<string> errors) : base(errors) { }
}
