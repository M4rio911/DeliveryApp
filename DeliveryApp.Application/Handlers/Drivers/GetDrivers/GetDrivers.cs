using DeliveryApp.Application.Interfaces.Mediator;

namespace DeliveryApp.Application.Handlers.Drivers.GetDrivers;

public class GetDrivers : IQuery<GetDriversResponse>
{
    public bool? OnlyActive { get; set; } = false;
    public GetDrivers(GetDriversParameters parameters)
    {
        OnlyActive = parameters.OnlyActive;
    }
}