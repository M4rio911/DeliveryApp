using DeliveryApp.Application.Interfaces.Repositories;
using DeliveryApp.Domain.Entities;

namespace DeliveryApp.Infrastructure.Repositories;

public class DictionaryRepository : IDictionaryRepository
{
    public Task<Dictionary> AddDictioanryAsync(Dictionary dictionary)
    {
        throw new NotImplementedException();
    }

    public Task<Dictionary> GetDictioanryByIdAsync(int dictionaryId)
    {
        throw new NotImplementedException();
    }
}
