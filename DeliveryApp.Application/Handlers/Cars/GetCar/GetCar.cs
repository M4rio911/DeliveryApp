using DeliveryApp.Application.Interfaces.Mediator;

namespace DeliveryApp.Application.Handlers.Cars.GetCar;

public class GetCar : IQuery<GetCarResponse>
{
    public int CarId { get; set; }
    public GetCar(GetCarParameters parameters)
    {
        CarId = parameters.CarId;
    }
}