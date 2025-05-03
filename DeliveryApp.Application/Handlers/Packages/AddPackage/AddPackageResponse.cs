using DeliveryApp.Application.Handlers.BaseModel;

namespace DeliveryApp.Application.Handlers.Packages.AddPackage;

public class AddPackageResponse : BaseResponse
{
    public int NewPackageId { get; set; }
    public int NewPaymentId { get; set; }
    public AddPackageResponse() : base(true, null, true) { }
    public AddPackageResponse(string error) : base(error) { }
}
