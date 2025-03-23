using DeliveryApp.Application.Dto.Countries;
using DeliveryApp.Application.Dto.Payments;
using DeliveryApp.Application.Handlers.BaseModel;
using DeliveryApp.Domain.Entities;

namespace DeliveryApp.Application.Handlers.Packages.AssignPackage;

public class AssignPackageResponse : BaseResponse
{
    public AssignPackageResponse() : base(true, null) { }
    public AssignPackageResponse(string error) : base(error) { }
}
