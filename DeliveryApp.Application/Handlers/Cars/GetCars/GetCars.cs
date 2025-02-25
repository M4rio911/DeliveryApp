using DeliveryApp.Application.Interfaces.Mediator;

namespace DeliveryApp.Application.Handlers.Cars.GetCars;

public class GetCars : IQuery<GetCarsResponse>
{
    public bool IncludeAssigned { get; set; } = true;
    public GetCars(GetCarsParameters parameters)
    {
        IncludeAssigned = parameters.IncludeAssigned;
    }
}