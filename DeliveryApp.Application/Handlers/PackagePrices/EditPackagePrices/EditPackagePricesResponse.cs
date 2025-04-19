using DeliveryApp.Application.Dto.PackagePrices;
using DeliveryApp.Application.Handlers.BaseModel;

namespace DeliveryApp.Application.Handlers.PackagePrices.EditPackagePrices;

public class EditPackagePricesResponse : BaseResponse
{
    public EditPackagePricesResponse() : base(true, null, true) { }
    public EditPackagePricesResponse(string error) : base(error) { }
}
