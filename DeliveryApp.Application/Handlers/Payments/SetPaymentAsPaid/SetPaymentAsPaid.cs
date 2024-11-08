using static DeliveryApp.Application.Interfaces.Mediator.ICommand;

namespace DeliveryApp.Application.Handlers.Payments.SetPaymentAsPaid;

public class SetPaymentAsPaid : ICommand<SetPaymentAsPaidResponse>
{
    public int PaymentId { get; set; }

    public SetPaymentAsPaid(SetPaymentAsPaidParameters parameters)
    {
        PaymentId = parameters.PaymentId;
    }
}