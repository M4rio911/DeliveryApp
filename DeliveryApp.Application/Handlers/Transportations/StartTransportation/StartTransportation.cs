using DeliveryApp.Application.Interfaces.Mediator;

namespace DeliveryApp.Application.Handlers.Transportations.StartTransportation;

public class StartTransportation: IQuery<StartTransportationResponse>
{
    public DateTime SelectedDate { get; set; }
    public StartTransportation(StartTransportationParameters parameters)
    {
        SelectedDate = parameters.SelectedDate;
    }
}