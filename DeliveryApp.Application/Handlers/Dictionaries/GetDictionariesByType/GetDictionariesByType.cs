using DeliveryApp.Application.Interfaces.Mediator;

namespace DeliveryApp.Application.Handlers.Dictionaries.GetDictionariesByType;

public class GetDictionariesByType : IQuery<GetDictionariesByTypeResponse>
{
    public int DictionaryTypeId { get; set; }
    public GetDictionariesByType(GetDictionariesByTypeParameters parameters)
    {
        DictionaryTypeId = parameters.DictionaryTypeId;
    }
}