using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Infrastructure.Models;

namespace DeliveryApp.Application.Handlers.Users.GetAllUsers;

public class GetAllUsers : IQuery<GetAllUsersResponse>
{
    public int UserTypeId {  get; set; }
    public GetAllUsers(GetAllUsersParameters parameters)
    {
        UserTypeId = parameters.UserTypeId;
    }
}
