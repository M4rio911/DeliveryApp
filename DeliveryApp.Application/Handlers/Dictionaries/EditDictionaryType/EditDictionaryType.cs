using static DeliveryApp.Application.Interfaces.Mediator.ICommand;

namespace DeliveryApp.Application.Handlers.Dictionaries.EditDictionaryType;

public class EditDictionaryType : ICommand<EditDictionaryTypeResponse>
{
    public int DictionaryTypeId { get; set; }
    public string Name { get; set; }
    public EditDictionaryType(EditDictionaryTypeParameters parameters)
    {
        DictionaryTypeId = parameters.DictionaryTypeId;
        Name = parameters.Name;
    }
}
