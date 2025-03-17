using DeliveryApp.Application.Interfaces.Mediator;

namespace DeliveryApp.Application.Handlers.Transportations.GetTransportation;

public class GetTransportations : IQuery<GetTransportationsResponse>
{
    public DateTime SelectedDate { get; set; }
    public GetTransportations(GetTransportationsParameters parameters)
    {
        SelectedDate = parameters.SelectedDate;
    }
}