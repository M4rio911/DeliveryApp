using DeliveryApp.Domain.Entities;

namespace DeliveryApp.Application.Interfaces.Repositories;

public interface IDictionaryRepository
{
    Task<Dictionary> AddDictioanryAsync(Dictionary dictionary);
    Task DeleteAsync(Dictionary dictionary);
    Task<Dictionary> GetByIdAsync(int id);
    Task<Dictionary> GetByIdNTAsync(int id);
    Task<Dictionary> GetDictionary(string type, string name);
    Task<Dictionary> GetDefaultDictionaryNTAsync(string type);
}
