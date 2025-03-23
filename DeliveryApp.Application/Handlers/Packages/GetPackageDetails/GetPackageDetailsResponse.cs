using DeliveryApp.Application.Dto.Packages;
using DeliveryApp.Application.Handlers.BaseModel;

namespace DeliveryApp.Application.Handlers.Packages.GetPackageDetails;

public class GetPackageDetailsResponse : BaseResponse
{
    public GetDriverTransportationPackageDto PackageDetails { get; set; }

    public GetPackageDetailsResponse() : base(true, null) { }
    public GetPackageDetailsResponse(string error) : base(error) { }
}
