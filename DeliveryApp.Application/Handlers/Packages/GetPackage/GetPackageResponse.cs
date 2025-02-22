using DeliveryApp.Application.Dto.Packages;
using DeliveryApp.Application.Handlers.BaseModel;

namespace DeliveryApp.Application.Handlers.Packages.GetPackage;

public class GetPackageResponse : BaseResponse
{
    public GetPackageDto Package { get; set; }

    public GetPackageResponse() : base(true, null) { }
    public GetPackageResponse(string error) : base(error) { }
}
