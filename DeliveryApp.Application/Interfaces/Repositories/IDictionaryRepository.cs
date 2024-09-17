using DeliveryApp.Domain.Entities;

namespace DeliveryApp.Application.Interfaces.Repositories;

public interface IDictionaryRepository
{
    Task<Dictionary> GetDictioanryByIdAsync(int dictionaryId);
    Task<Dictionary> AddDictioanryAsync(Dictionary dictionary);
}
