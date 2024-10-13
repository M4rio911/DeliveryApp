using static DeliveryApp.Application.Interfaces.Mediator.ICommand;

namespace DeliveryApp.Application.Handlers.Dictionaries.RemoveDictionary;

public class RemoveDictionary : ICommand<RemoveDictionaryResponse>
{
    public int Id { get; set; }
    public RemoveDictionary(RemoveDictionaryParameters parameters)
    {
        Id = parameters.Id;
    }
}
