using static DeliveryApp.Application.Interfaces.Mediator.ICommand;

namespace DeliveryApp.Application.Handlers.Dictionaries.RemoveDictionary;

public class RemoveDictionary : ICommand<RemoveDictionaryResponse>
{
    public int DictionaryTypeId { get; set; }
    public int DictionaryId { get; set; }
    public RemoveDictionary(RemoveDictionaryParameters parameters)
    {
        DictionaryId = parameters.DictionaryId;
        DictionaryTypeId = parameters.DictionaryTypeId;
    }
}
