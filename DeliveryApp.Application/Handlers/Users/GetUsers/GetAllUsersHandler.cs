using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Persistance;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.Users.GetAllUsers;

public class GetAllUsersHandler : IQueryHandler<GetAllUsers, GetAllUsersResponse>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly DbContextOptions<DeliveryDbContext> _options;

    public GetAllUsersHandler(DbContextOptions<DeliveryDbContext> options, IHttpContextAccessor httpContextAccessor)
    {
        _options = options;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<GetAllUsersResponse> Handle(GetAllUsers request, CancellationToken cancellationToken)
    {
        return new GetAllUsersResponse();
    }

}
