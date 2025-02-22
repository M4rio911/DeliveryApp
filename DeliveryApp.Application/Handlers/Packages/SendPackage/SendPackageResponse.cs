using DeliveryApp.Application.Dto.Countries;
using DeliveryApp.Application.Dto.Payments;
using DeliveryApp.Application.Handlers.BaseModel;
using DeliveryApp.Domain.Entities;

namespace DeliveryApp.Application.Handlers.Packages.SendPackage;

public class SendPackageResponse : BaseResponse
{
    public SendPackageResponse() : base(true, null) { }
    public SendPackageResponse(string error) : base(error) { }
}
