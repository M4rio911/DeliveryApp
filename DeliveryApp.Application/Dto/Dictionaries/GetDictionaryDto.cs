namespace DeliveryApp.Application.Dto.Dictionaries;

public class GetDictionaryDto
{
    public string Name { get; set; }
    public int DictionaryTypeId { get; set; }
    public int DictionaryId { get; set; }
    public bool IsDefault { get; set; }
}
