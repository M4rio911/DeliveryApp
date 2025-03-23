using DeliveryApp.Application.Interfaces.Mediator;

namespace DeliveryApp.Application.Handlers.Transportations.GetDriverDailyTransportations;

public class GetDriverDailyTransportations : IQuery<GetDriverDailyTransportationsResponse>
{
    public int TransportationId { get; set; }
    public GetDriverDailyTransportations(GetDriverDailyTransportationsParameters parameters)
    {
        TransportationId = parameters.TransportationId;
    }
}