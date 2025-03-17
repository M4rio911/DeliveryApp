using DeliveryApp.Application.Interfaces.Mediator;

namespace DeliveryApp.Application.Handlers.Transportations.GetDriverTransportations;

public class GetDriverTransportations : IQuery<GetDriverTransportationsResponse>
{
    public DateTime SelectedDate { get; set; }
    public GetDriverTransportations(GetDriverTransportationsParameters parameters)
    {
        SelectedDate = parameters.SelectedDate;
    }
}