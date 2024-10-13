using DeliveryApp.Application.Interfaces.Mediator;

namespace DeliveryApp.Application.Handlers.Dictionaries.GetDictionaryType;

public class GetDictionaryType : IQuery<GetDictionaryTypeResponse>
{
    public int DictionaryTypeId { get; set; }
    public GetDictionaryType(GetDictionaryTypeParameters parameters)
    {
        DictionaryTypeId = parameters.DictionaryTypeId;
    }
}