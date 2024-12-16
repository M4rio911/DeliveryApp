using DeliveryApp.Application.Dto.Users;
using DeliveryApp.Application.Handlers.BaseModel;

namespace DeliveryApp.Application.Handlers.User.ChangeActiveStatus;

public class ChangeActiveStatusResponse : BaseResponse
{
    public ChangeActiveStatusResponse() : base(true, null) 
    {
    }
    public ChangeActiveStatusResponse(string error) : base(error) { }
}
