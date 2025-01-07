using static DeliveryApp.Application.Interfaces.Mediator.ICommand;

namespace DeliveryApp.Application.Handlers.Dictionaries.EditDictionary;

public class EditDictionary : ICommand<EditDictionaryResponse>
{
    public int DictionaryId { get; set; }
    public int DictionaryTypeId { get; set; }
    public string Name { get; set; }
    public bool IsDefault { get; set; }
    public EditDictionary(EditDictionaryParameters parameters)
    {
        DictionaryId = parameters.DictionaryId;
        DictionaryTypeId = parameters.DictionaryTypeId;
        Name = parameters.Name;
        IsDefault = parameters.IsDefault;
    }
}
