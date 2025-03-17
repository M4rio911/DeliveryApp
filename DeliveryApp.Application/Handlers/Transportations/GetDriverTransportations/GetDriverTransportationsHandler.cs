using DeliveryApp.Application.Dto.Payments;
using DeliveryApp.Application.Dto.Transportations;
using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Application.Interfaces.Repositories;
using DeliveryApp.Infrastructure.Models;
using DeliveryApp.Persistance;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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
            return new GetDriverTransportationsResponse("Driver was not found");
        }

        var transportation = await _context.Transportations
            .Where(x =>
                x.DateOfTransport.Date == request.SelectedDate.Date &&
                x.AssignedDriverId == driver.Id)
            .FirstOrDefaultAsync();

        var transportId = transportation?.Id ?? 0;
         
        var response = await _context.TransportationItems
            .Where(x => x.TransportationId == transportId)
            .Include(x => x.Package)
                .ThenInclude(x => x.Destination)
                    .ThenInclude(x => x.Country)
            .Include(x => x.Package)
                .ThenInclude(x => x.Sender)
            .Include(x => x.Package)
                .ThenInclude(x => x.Reciver)
        .Select(x => new GetDriverTransportationPackageDto
        {
            PackageId = x.Package.Id,
            SenderEmail = x.Package.Sender.Email,
            ReciverEmail = x.Package.Reciver.Email,
            PackageStatusId = x.Package.PackageStatusId,
            PackageTypeId = x.Package.PackageTypeId,
            Country = x.Package.Destination.Country.Name,
            PostCode = x.Package.Destination.PostCode,
            City = x.Package.Destination.City,
            Street = x.Package.Destination.Street,
            Number = x.Package.Destination.Number,
            AddressTypeId = x.Package.Destination.AddressTypeId
        })
        .ToListAsync();

        var toCollectStatus = (await _dictionaryRepository.GetDictionary(
            DictionaryTypeEnum.PackageStatus.ToString(),
            PackageStatusEnum.AssignedToCollect.ToString())).Id;

        var toDeliveryStatus = (await _dictionaryRepository.GetDictionary(
            DictionaryTypeEnum.PackageStatus.ToString(),
            PackageStatusEnum.AssignedToDelivery.ToString())).Id;

        var collectList = response.Where(x => x.PackageStatusId == toCollectStatus).ToList();
        var deliverList = response.Where(x => x.PackageStatusId == toDeliveryStatus).ToList();

        var transportationResponse = new GetDriverTransportationsDto()
        {
            PackagesToCollect = collectList,
            PackagesToDelivery = deliverList,
            TransportationStatus = transportation?.TransportationStatusId ?? 0,
            DateOfTransport = request.SelectedDate.Date
        };

        return new GetDriverTransportationsResponse()
        {
            Transportation = transportationResponse
        };
    }
}
