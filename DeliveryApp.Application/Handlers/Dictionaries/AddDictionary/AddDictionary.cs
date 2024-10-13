using static DeliveryApp.Application.Interfaces.Mediator.ICommand;

namespace DeliveryApp.Application.Handlers.Dictionaries.AddDictionary;

public class AddDictionary : ICommand<AddDictionaryResponse>
{
    public int DictionaryTypeId { get; set; }
    public string Name { get; set; }
    public bool IsDefault { get; set; }
    public AddDictionary(AddDictionaryParameters parameters)
    {
        DictionaryTypeId = parameters.DictionaryTypeId;
        Name = parameters.Name;
        IsDefault = parameters.IsDefault;
    }
}
