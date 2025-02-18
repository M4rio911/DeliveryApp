using DeliveryApp.Application.Handlers.BaseModel;

namespace DeliveryApp.Application.Handlers.Packages.AddPackage;

public class AddPackageResponse : BaseResponse
{
    public int NewPackageId { get; set; }
    public AddPackageResponse() : base(true, null) { }
    public AddPackageResponse(string error) : base(error) { }
}
