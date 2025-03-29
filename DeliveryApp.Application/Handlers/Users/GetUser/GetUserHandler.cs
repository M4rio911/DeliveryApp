using DeliveryApp.Application.Dto.Users;
using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Persistance;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DeliveryApp.Application.Handlers.Users.GetUser;

public class GetUserHandler : IQueryHandler<GetUser, GetUserResponse>
{
    private readonly DeliveryDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetUserHandler(IHttpContextAccessor httpContextAccessor, DeliveryDbContext deliveryDbContext)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = deliveryDbContext;
    }

    public async Task<GetUserResponse> Handle(GetUser request, CancellationToken cancellationToken)
    {
        if (request.CurrentUser.GetValueOrDefault() == false && request.UserId == null)
            return new GetUserResponse("No user data was passed");


        var userId = request.CurrentUser.GetValueOrDefault()
            ? _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            : request.UserId;

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
            .Where(x => x.Id == userId)
            .FirstOrDefaultAsync();

        return new GetUserResponse()
        {
            User = response
        };
    }

}
