using static DeliveryApp.Application.Interfaces.Mediator.ICommand;

namespace DeliveryApp.Application.Handlers.Dictionaries.RemoveDictionaryType;

public class RemoveDictionaryType : ICommand<RemoveDictionaryTypeResponse>
{
    public int Id { get; set; }
    public RemoveDictionaryType(RemoveDictionaryTypeParameters parameters)
    {
        Id = parameters.Id;
    }
}
