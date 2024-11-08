using DeliveryApp.Domain.Entities;

namespace DeliveryApp.Application.Handlers.Payments.AddPayment;

public class AddPaymentParameters
{
    public int PackageId { get; set; }
    public int PaymentTypeId { get; set; }
}