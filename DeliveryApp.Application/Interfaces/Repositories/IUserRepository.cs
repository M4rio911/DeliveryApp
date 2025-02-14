using DeliveryApp.Domain.Entities;

namespace DeliveryApp.Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsers();
    Task<User> GetUserByEmailNTAsync(string email);
}
