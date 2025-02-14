using DeliveryApp.Application.Dto.PackagePrices;
using DeliveryApp.Application.Handlers.BaseModel;

namespace DeliveryApp.Application.Handlers.PackagePrices.GetPackagePrices;

public class GetPackagePricesResponse : BaseResponse
{
    public List<GetPackagePricesDto> PackagePrices { get; set; }

    public GetPackagePricesResponse() : base(true, null) { }
    public GetPackagePricesResponse(List<string> errors) : base(errors) { }
}
