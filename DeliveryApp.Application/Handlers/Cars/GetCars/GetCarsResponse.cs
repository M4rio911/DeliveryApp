using DeliveryApp.Application.Dto.Cars;
using DeliveryApp.Application.Handlers.BaseModel;

namespace DeliveryApp.Application.Handlers.Cars.GetCars;

public class GetCarsResponse : BaseResponse
{
    public List<GetCarsDto> Cars { get; set; }

    public GetCarsResponse() : base(true, null) { }
    public GetCarsResponse(List<string> errors) : base(errors) { }
}
