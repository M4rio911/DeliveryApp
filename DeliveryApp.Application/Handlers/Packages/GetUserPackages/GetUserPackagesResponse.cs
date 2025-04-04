using DeliveryApp.Application.Dto.Packages;
using DeliveryApp.Application.Handlers.BaseModel;

namespace DeliveryApp.Application.Handlers.Packages.GetUserPackages;

public class GetUserPackagesResponse : BaseResponse
{
    public IEnumerable<GetPackageDto> PostedFromUser { get; set; }
    public IEnumerable<GetPackageDto> PostedToUser { get; set; }

    public GetUserPackagesResponse() : base(true, null) { }
    public GetUserPackagesResponse(string error) : base(error) { }
}
