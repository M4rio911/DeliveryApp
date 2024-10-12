using static DeliveryApp.Application.Interfaces.Mediator.ICommand;

namespace DeliveryApp.Application.Handlers.Dictionaries.AddDictionaryType;

public class AddDictionaryType : ICommand<AddDictionaryTypeResponse>
{
    public string Name { get; set; }
    public AddDictionaryType(AddDictionaryTypeParameters parameters)
    {
        Name = parameters.Name;
    }
}
