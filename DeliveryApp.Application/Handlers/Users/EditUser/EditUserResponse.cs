using DeliveryApp.Application.Dto.Users;
using DeliveryApp.Application.Handlers.BaseModel;

namespace DeliveryApp.Application.Handlers.User.EditUser;

public class EditUserResponse : BaseResponse
{
    public GetUserDto User { get; set; }
    public EditUserResponse(Domain.Entities.User user) : base(true, null, true) 
    {
        User = new GetUserDto
        {
            Id = user.Id,
            UserName = user.UserName,
            ActiveStatus = user.ActiveStatus,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            UserType = user.UserTypeId,
        };
    }
    public EditUserResponse(string error) : base(error) { }
}
