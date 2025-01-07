namespace DeliveryApp.Application.Handlers.Dictionaries.EditDictionary;

public class EditDictionaryParameters
{
    public int DictionaryId { get; set; }
    public int DictionaryTypeId { get; set; }
    public string Name { get; set; }
    public bool IsDefault { get; set; }
}
