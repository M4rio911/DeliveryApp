using DeliveryApp.Application.Interfaces.Mediator;

namespace DeliveryApp.Application.Handlers.Drivers.GetDriverByUserId;

public class GetDriver : IQuery<GetDriverResponse>
{
    public string UserId { get; set; }
    public GetDriver(GetDriverParameters parameters)
    {
        UserId = parameters.UserId;
    }
}