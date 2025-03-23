using DeliveryApp.Application.Interfaces.Mediator;

namespace DeliveryApp.Application.Handlers.Packages.GetPackageDetails;

public class GetPackageDetails : IQuery<GetPackageDetailsResponse>
{
    public int PackageId { get; set; }
    public GetPackageDetails(GetPackageDetailsParameters parameters)
    {
        PackageId = parameters.PackageId;
    }
}