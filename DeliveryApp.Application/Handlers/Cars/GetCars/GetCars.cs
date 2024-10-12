using DeliveryApp.Application.Handlers.Cars.GetCars;
using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Domain.Entities;

namespace DeliveryApp.Application.Handlers.Cars.GetCars;

public class GetCars : IQuery<GetCarsResponse>
{
    public GetCars()
    {
    }
}