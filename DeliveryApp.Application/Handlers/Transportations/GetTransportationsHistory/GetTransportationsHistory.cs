using DeliveryApp.Application.Interfaces.Mediator;

namespace DeliveryApp.Application.Handlers.Transportations.GetTransportationsHistory;

public class GetTransportationsHistory : IQuery<GetTransportationsHistoryResponse>
{
    public GetTransportationsHistory(GetTransportationsHistoryParameters parameters)
    {

    }
}