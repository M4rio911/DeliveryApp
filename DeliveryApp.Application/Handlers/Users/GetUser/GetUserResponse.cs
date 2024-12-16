using DeliveryApp.Application.Dto.Users;
using DeliveryApp.Application.Handlers.BaseModel;

namespace DeliveryApp.Application.Handlers.Users.GetUser;

public class GetUserResponse : BaseResponse
{
    public GetUserDto User { get; set; }
    public GetUserResponse() : base(true, null) { }
}
