using DeliveryApp.Application.Dto.Payments;
using DeliveryApp.Application.Dto.Transportations;
using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Application.Interfaces.Repositories;
using DeliveryApp.Persistance;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.Transportations.GetDriverTransportations;

public class GetDriverTransportationsHandler : IQueryHandler<GetDriverTransportations, GetDriverTransportationsResponse>
{
    private readonly DeliveryDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IDictionaryRepository _dictionaryRepository;

    public GetDriverTransportationsHandler(DeliveryDbContext deliveryDbContext, IHttpContextAccessor httpContext, IDictionaryRepository dictionaryRepository)
    {
        _context = deliveryDbContext;
        _httpContextAccessor = httpContext;
        _dictionaryRepository = dictionaryRepository;
    }

    public async Task<GetDriverTransportationsResponse> Handle(GetDriverTransportations request, CancellationToken cancellationToken)
    {
        var user = _httpContextAccessor.HttpContext?.User.Identities.FirstOrDefault().Name;
        if (user == null)
        {
            throw new UnauthorizedAccessException("User is not authenticated");
        }

        var driver = await _context.Drivers
            .Where(x => x.BaseUserId == user)
            .FirstOrDefaultAsync(cancellationToken);

        if (driver == null)
        {
            return new GetDriverTransportationsResponse("Driver was not found");
        }

        var response = await _context.Transportations
            .Include(x => x.TransportationItems)
            .Where(x => 
                x.DateOfTransport.Date == request.SelectedDate.Date &&
                x.AssignedDriverId == driver.Id)
            .Select(x => new GetDriverTransportationsDto()
            {
                TransportationId = x.Id,
                DateOfTransport = x.DateOfTransport,
            }).ToListAsync(cancellationToken);

        return new GetDriverTransportationsResponse()
        {
            Transportations = response
        };
    }
}
