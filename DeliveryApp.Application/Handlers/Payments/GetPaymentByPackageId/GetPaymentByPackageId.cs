using DeliveryApp.Application.Interfaces.Mediator;

namespace DeliveryApp.Application.Handlers.Payments.GetPaymentByPackageId;

public class GetPaymentByPackageId : IQuery<GetPaymentByPackageIdResponse>
{
    public int PackageId { get; set; }
    public GetPaymentByPackageId(GetPaymentByPackageIdParameters parameters)
    {
        PackageId = parameters.PackageId;
    }
}