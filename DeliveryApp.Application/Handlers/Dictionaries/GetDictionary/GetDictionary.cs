using DeliveryApp.Application.Interfaces.Mediator;

namespace DeliveryApp.Application.Handlers.Dictionaries.GetDictionary;

public class GetDictionary : IQuery<GetDictionaryResponse>
{
    public int DictionaryId { get; set; }
    public GetDictionary(GetDictionaryParameters parameters)
    {
        DictionaryId = parameters.DictionaryId;
    }
}