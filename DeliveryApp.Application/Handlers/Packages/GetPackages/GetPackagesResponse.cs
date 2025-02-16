using DeliveryApp.Application.Dto.Packages;
using DeliveryApp.Application.Handlers.BaseModel;

namespace DeliveryApp.Application.Handlers.Packages.GetPackages;

public class GetPackagesResponse : BaseResponse
{
    public IEnumerable<GetPackageDto> Packages { get; set; }

    public GetPackagesResponse() : base(true, null) { }
    public GetPackagesResponse(string error) : base(error) { }
}
