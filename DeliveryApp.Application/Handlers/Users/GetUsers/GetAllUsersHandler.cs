﻿using DeliveryApp.Application.Dto.Cars;
using DeliveryApp.Application.Dto.Users;
using DeliveryApp.Application.Handlers.Cars.GetCars;
using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Persistance;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.Users.GetAllUsers;

public class GetAllUsersHandler : IQueryHandler<GetAllUsers, GetAllUsersResponse>
{
    private readonly DeliveryDbContext _context;

    public GetAllUsersHandler(DeliveryDbContext deliveryDbContext)
    {
        _context = deliveryDbContext;
    }

    public async Task<GetAllUsersResponse> Handle(GetAllUsers request, CancellationToken cancellationToken)
    {
        var response = await _context.Users
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
            .OrderByDescending(x => x.ActiveStatus)
            .ThenBy(x => x.UserName)
            .ToListAsync();

        return new GetAllUsersResponse()
        {
            Users = response
        };
    }

}
