using DeliveryApp.Application.Handlers.BaseModel;

namespace DeliveryApp.Application.Handlers.Packages.CollectPackage;

public class CollectPackageResponse : BaseResponse
{
    public CollectPackageResponse() : base(true, null, true) { }
    public CollectPackageResponse(string error) : base(error) { }
}
