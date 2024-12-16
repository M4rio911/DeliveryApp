using DeliveryApp.Application.Interfaces.Mediator;

namespace DeliveryApp.Application.Handlers.Users.GetUser;

public class GetUser: IQuery<GetUserResponse>
{
    public string UserId { get; set; }
    public GetUser(GetUserParameters getUserParameters)
    {
        UserId = getUserParameters.UserId;
    }
}
