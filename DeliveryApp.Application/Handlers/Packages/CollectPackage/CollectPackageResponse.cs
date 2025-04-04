using DeliveryApp.Application.Handlers.BaseModel;

namespace DeliveryApp.Application.Handlers.Packages.CollectPackage;

public class CollectPackageResponse : BaseResponse
{
    public CollectPackageResponse() : base(true, null) { }
    public CollectPackageResponse(string error) : base(error) { }
}
