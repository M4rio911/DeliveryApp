using DeliveryApp.Application.Interfaces.Mediator;

namespace DeliveryApp.Application.Handlers.Transportations.FinishTransportation;

public class FinishTransportation: IQuery<FinishTransportationResponse>
{
    public int TransportationId { get; set; }
    public FinishTransportation(FinishTransportationParameters parameters)
    {
        TransportationId = parameters.TransportationId;
    }
}