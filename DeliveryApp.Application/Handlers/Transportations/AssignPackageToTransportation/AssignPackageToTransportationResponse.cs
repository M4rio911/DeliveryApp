﻿using DeliveryApp.Application.Handlers.BaseModel;

namespace DeliveryApp.Application.Handlers.Transportations.AssignPackageToTransportation;

public class AssignPackageToTransportationResponse : BaseResponse
{
    public AssignPackageToTransportationResponse() : base(true, null, true) { }
    public AssignPackageToTransportationResponse(string error) : base(error) { }
}
