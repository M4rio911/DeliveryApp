using static DeliveryApp.Application.Interfaces.Mediator.ICommand;

namespace DeliveryApp.Application.Handlers.User.EditUser;

public class EditUser : ICommand<EditUserResponse>
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public int UserType { get; set; }

    public EditUser(EditUserParameters parameters)
    {
        Id = parameters.Id;
        UserName = parameters.UserName;
        FirstName = parameters.FirstName;
        LastName = parameters.LastName; 
        Email = parameters.Email;
        PhoneNumber = parameters.PhoneNumber;
        UserType = parameters.UserType;
    }
}