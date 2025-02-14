using DeliveryApp.Application.Handlers.Addresses.AddAddress;
using DeliveryApp.Application.Interfaces.Repositories;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Persistance;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Infrastructure.Repositories;

public class AddressRepository : DeliveryDbContextFactory, IAddressRepository
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly DbContextOptions<DeliveryDbContext> _options;
    public AddressRepository(DbContextOptions<DeliveryDbContext> options, IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _options = options;
    }
    public async Task<Address> AddGuestAddress(AddAddressParameters address)
    {
        var context = CreateNewInstance(_options);

        var newAddress = new Address
        {
            UserId =  null,
            Name = "GuestAddress",
            CountryId = address.CountryId,
            PostCode = address.PostCode,
            City = address.City,
            Street = address.Street,
            Number = address.Number,
            AddressTypeId = address.AddressTypeId,
            GuestAddress = true,
            Created = DateTime.UtcNow,
            CreatedBy = "GuestAddress",
            ModifiedBy = "GuestAddress"
        };

        context.Address.Add(newAddress);

        await context.SaveChangesAsync();
        return newAddress;
    }
}
