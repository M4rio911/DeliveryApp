using DeliveryApp.Application.Interfaces.Repositories;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Persistance;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Infrastructure.Repositories;

public class DictionaryRepository : DeliveryDbContextFactory, IDictionaryRepository
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly DbContextOptions<DeliveryDbContext> _options;

    public DictionaryRepository(DbContextOptions<DeliveryDbContext> options, IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _options = options;
    }
    public async Task<Dictionary> AddDictioanryAsync(Dictionary dictionary)
    {
        var context = CreateNewInstance(_options);

        await context.AddAsync(dictionary);
        await context.SaveChangesWithAuditableEntityAsync(_httpContextAccessor);

        return dictionary;
    }

    public async Task DeleteAsync(Dictionary dictionary)
    {
        var context = CreateNewInstance(_options);

        context.Entry(dictionary).State = EntityState.Deleted;
        await context.SaveChangesWithAuditableEntityAsync(_httpContextAccessor);
    }

    public async Task<Dictionary> GetByIdAsync(int id)
    {
        var context = CreateNewInstance(_options);

        return await context.Dictionaries
           .Where(w => w.Id == id)
           .AsNoTracking()
           .FirstOrDefaultAsync();
    }

    public async Task<Dictionary> GetByIdNTAsync(int id)
    {
        var context = CreateNewInstance(_options);

        return await context.Dictionaries
           .Include(i => i.DictionaryType)
           .Where(w => w.Id == id)
           .AsNoTracking()
           .FirstOrDefaultAsync();
    }

    public async Task<Dictionary> GetDictionary(string type, string name)
    {
        var context = CreateNewInstance(_options);

        return await context.Dictionaries
            .Include(d => d.DictionaryType)
            .Where(d => d.DictionaryType.Name.Equals(type) && d.Name.Equals(name))
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    public async Task<Dictionary> GetDictionary(string type, int dictionaryId)
    {
        var context = CreateNewInstance(_options);

        return await context.Dictionaries
            .Include(d => d.DictionaryType)
            .Where(d => d.DictionaryType.Name.Equals(type) && d.Id == dictionaryId)
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    public async Task<Dictionary> GetDefaultDictionaryNTAsync(string type)
    {
        var context = CreateNewInstance(_options);

        return await context.Dictionaries
            .Include(d => d.DictionaryType)
            .Where(d => d.DictionaryType.Name.Equals(type) && d.IsDefault)
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }
}
