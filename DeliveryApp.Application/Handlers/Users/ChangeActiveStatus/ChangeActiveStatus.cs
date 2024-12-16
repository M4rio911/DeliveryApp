using static DeliveryApp.Application.Interfaces.Mediator.ICommand;

namespace DeliveryApp.Application.Handlers.User.ChangeActiveStatus;

public class ChangeActiveStatus : ICommand<ChangeActiveStatusResponse>
{
    public string Id { get; set; }
    public bool NewActiveStatus { get; set; }

    public ChangeActiveStatus(ChangeActiveStatusParameters parameters)
    {
        Id = parameters.Id;
        NewActiveStatus = parameters.NewActiveStatus;
    }
}