using DeliveryApp.Application.Dto.Payments;
using DeliveryApp.Application.Dto.Transportations;
using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Application.Interfaces.Repositories;
using DeliveryApp.Infrastructure.Models;
using DeliveryApp.Persistance;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DeliveryApp.Application.Handlers.Transportations.GetTransportationsHistory;

public class GetTransportationsHistoryHandler : IQueryHandler<GetTransportationsHistory, GetTransportationsHistoryResponse>
{
    private readonly DeliveryDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IDictionaryRepository _dictionaryRepository;

    public GetTransportationsHistoryHandler(DeliveryDbContext deliveryDbContext, IHttpContextAccessor httpContext, IDictionaryRepository dictionaryRepository)
    {
        _context = deliveryDbContext;
        _httpContextAccessor = httpContext;
        _dictionaryRepository = dictionaryRepository;
    }

    public async Task<GetTransportationsHistoryResponse> Handle(GetTransportationsHistory request, CancellationToken cancellationToken)
    {
        var user = _httpContextAccessor.HttpContext?.User.Identities.FirstOrDefault();
        if (user == null)
        {
            throw new UnauthorizedAccessException("User is not authenticated");
        }
        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var driver = await _context.Drivers
            .Where(x => x.BaseUserId == userId)
            .FirstOrDefaultAsync(cancellationToken);

        if (driver == null)
        {
            return new GetTransportationsHistoryResponse("Driver was not found");
        }

        var transportations = await _context.Transportations
            .Where(x => x.AssignedDriverId == driver.Id)
            .Select(x => new GetTransportationsHistoryDto
            {
                TransportationId = x.Id,
                DateOfTransport = x.DateOfTransport,
                TransportationStatus = x.TransportationStatusId
            })
            .ToListAsync(cancellationToken);

        return new GetTransportationsHistoryResponse()
        {
            Transportations = transportations
        };
    }
}
