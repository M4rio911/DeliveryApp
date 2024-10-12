using static DeliveryApp.Application.Interfaces.Mediator.ICommand;

namespace DeliveryApp.Application.Handlers.Cars.RemoveCar;

public class RemoveCar : ICommand<RemoveCarResponse>
{
    public int CarId { get; set; }

    public RemoveCar(RemoveCarParameters parameters)
    {
        CarId = parameters.CarId;
    }
}