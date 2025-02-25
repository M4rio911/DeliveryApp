using DeliveryApp.Application.Dto.Users;
using DeliveryApp.Application.Handlers.BaseModel;
using DeliveryApp.Domain.Entities;

namespace DeliveryApp.Application.Handlers.Drivers.EditDriver;

public class EditDriverResponse : BaseResponse
{
    public GetDriverDto Driver { get; set; }
    public EditDriverResponse(Driver driver) : base(true, null) 
    {
        Driver = new GetDriverDto
        {
            Id = driver.Id,
            BaseUserId = driver.BaseUserId,
            AssignedCarId = driver.AssignedCarId
        };
    }
    public EditDriverResponse(string error) : base(error) { }
}
