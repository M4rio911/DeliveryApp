using DeliveryApp.Application.Interfaces.Repositories;
using DeliveryApp.Domain.Entities;

namespace DeliveryApp.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    public Task<IEnumerable<User>> GetAllUsers()
    {
        throw new NotImplementedException();
    }
}
