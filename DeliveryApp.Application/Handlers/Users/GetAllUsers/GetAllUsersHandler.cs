using DeliveryApp.Application.Dto.Cars;
using DeliveryApp.Application.Dto.Users;
using DeliveryApp.Application.Handlers.Cars.GetCars;
using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Application.Interfaces.Repositories;
using DeliveryApp.Infrastructure.Models;
using DeliveryApp.Persistance;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.Users.GetAllUsers;

public class GetAllUsersHandler : IQueryHandler<GetAllUsers, GetAllUsersResponse>
{
    private readonly IDictionaryRepository _dictionaryRepository;
    private readonly DeliveryDbContext _context;

    public GetAllUsersHandler(IDictionaryRepository dictionaryRepository, DeliveryDbContext deliveryDbContext)
    {
        _dictionaryRepository = dictionaryRepository;
        _context = deliveryDbContext;
    }

    public async Task<GetAllUsersResponse> Handle(GetAllUsers request, CancellationToken cancellationToken)
    {
        int? userTypeFilter = request.UserTypeId == 0 ? null : 
            (await _dictionaryRepository.GetDictionary(DictionaryTypeEnum.UserType.ToString(), request.UserTypeId)).Id;

        var response = await _context.Users
            .Where(x => userTypeFilter == null || x.UserTypeId == userTypeFilter)
            .Select(x => new GetUserDto()
            {
                Id = x.Id,
                ActiveStatus = x.ActiveStatus,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                UserName = x.UserName,
                UserType = x.UserTypeId,
                PhoneNumber = x.PhoneNumber
            })
            //IF request.userType
            .OrderByDescending(x => x.ActiveStatus)
            .ThenBy(x => x.UserName)
            .ToListAsync();

        return new GetAllUsersResponse()
        {
            Users = response
        };
    }

}
