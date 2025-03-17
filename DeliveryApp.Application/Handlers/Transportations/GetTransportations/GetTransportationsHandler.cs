using DeliveryApp.Application.Dto.Payments;
using DeliveryApp.Application.Dto.Transportations;
using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Application.Interfaces.Repositories;
using DeliveryApp.Persistance;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.Transportations.GetTransportation;

public class GetTransportationsHandler : IQueryHandler<GetTransportations, GetTransportationsResponse>
{
    private readonly DeliveryDbContext _context;
    private readonly IDictionaryRepository _dictionaryRepository;

    public GetTransportationsHandler(DeliveryDbContext deliveryDbContext, IDictionaryRepository dictionaryRepository)
    {
        _context = deliveryDbContext;
        _dictionaryRepository = dictionaryRepository;
    }

    public async Task<GetTransportationsResponse> Handle(GetTransportations request, CancellationToken cancellationToken)
    {
        var response = await _context.Transportations
            .Where(x => x.DateOfTransport.Date == request.SelectedDate.Date)
            .Select(x => new GetTransportationsDto()
            {
                TransportationId = x.Id,
                DateOfTransport = x.DateOfTransport,
                AssignedDriverId = x.AssignedDriverId
            }).ToListAsync(cancellationToken);

        if (response == null)
            return new GetTransportationsResponse("No transportations was found under passed Id");

        return new GetTransportationsResponse()
        {
            Transportations = response
        };
    }
}
