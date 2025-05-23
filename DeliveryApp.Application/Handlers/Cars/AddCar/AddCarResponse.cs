﻿using DeliveryApp.Application.Handlers.BaseModel;

namespace DeliveryApp.Application.Handlers.Cars.AddCar;

public class AddCarResponse : BaseResponse
{
    public AddCarResponse() : base(true, null, true) { }
    public AddCarResponse(List<string> errors) : base(errors) { }
}
