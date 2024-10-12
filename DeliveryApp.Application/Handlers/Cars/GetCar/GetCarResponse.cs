using DeliveryApp.Application.Dto.Cars;
using DeliveryApp.Application.Handlers.BaseModel;

namespace DeliveryApp.Application.Handlers.Cars.GetCar;

public class GetCarResponse : BaseResponse
{
    public GetCarsDto Car { get; set; }

    public GetCarResponse() : base(true, null) { }
    public GetCarResponse(string error) : base(error) { }
}
