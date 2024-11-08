using DeliveryApp.Application.Interfaces.Mediator;

namespace DeliveryApp.Application.Handlers.Payments.GetPayment;

public class GetPayment : IQuery<GetPaymentResponse>
{
    public int PaymentId { get; set; }
    public GetPayment(GetPaymentParameters parameters)
    {
        PaymentId = parameters.PaymentId;
    }
}