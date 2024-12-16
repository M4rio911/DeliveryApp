using DeliveryApp.Application.Dto.Users;
using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Persistance;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.Users.GetUser;

public class GetUserHandler : IQueryHandler<GetUser, GetUserResponse>
{
    private readonly DeliveryDbContext _context;

    public GetUserHandler(DeliveryDbContext deliveryDbContext)
    {
        _context = deliveryDbContext;
    }

    public async Task<GetUserResponse> Handle(GetUser request, CancellationToken cancellationToken)
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
            .Where(x => x.Id == request.UserId)
            .FirstOrDefaultAsync();

        return new GetUserResponse()
        {
            User = response
        };
    }

}
