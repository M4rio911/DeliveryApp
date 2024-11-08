using static DeliveryApp.Application.Interfaces.Mediator.ICommand;

namespace DeliveryApp.Application.Handlers.Payments.AddPayment;

public class AddPayment : ICommand<AddPaymentResponse>
{
    public int PackageId { get; set; }
    public int PaymentTypeId { get; set; }

    public AddPayment(AddPaymentParameters parameters)
    {
        PackageId = parameters.PackageId;
        PaymentTypeId = parameters.PaymentTypeId;
    }
}