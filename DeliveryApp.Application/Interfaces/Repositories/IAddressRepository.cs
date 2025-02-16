using DeliveryApp.Application.Handlers.Addresses.AddAddress;
using DeliveryApp.Domain.Entities;

namespace DeliveryApp.Application.Interfaces.Repositories;

public interface IAddressRepository
{
    Task<Address> AddGuestAddress(AddAddressParameters address, string userName);
}
