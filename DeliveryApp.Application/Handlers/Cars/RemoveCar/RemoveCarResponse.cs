﻿using DeliveryApp.Application.Handlers.BaseModel;

namespace DeliveryApp.Application.Handlers.Cars.RemoveCar;

public class RemoveCarResponse : BaseResponse
{
    public RemoveCarResponse() : base(true, null, true) { }
    public RemoveCarResponse(string error) : base(error) { }
}
