using DeliveryApp.Application.Interfaces.Repositories;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Persistance;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Infrastructure.Repositories;

public class UserRepository : DeliveryDbContextFactory, IUserRepository
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly DbContextOptions<DeliveryDbContext> _options;
    public UserRepository(DbContextOptions<DeliveryDbContext> options, IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _options = options;
    }
    public Task<IEnumerable<User>> GetAllUsers()
    {
        throw new NotImplementedException();
    }

    public async Task<User> GetUserByEmailNTAsync(string email)
    {
        var context = CreateNewInstance(_options);

        var userDb = await context.Users
            .Where(u => u.Email == email)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        return userDb;
    }
}
