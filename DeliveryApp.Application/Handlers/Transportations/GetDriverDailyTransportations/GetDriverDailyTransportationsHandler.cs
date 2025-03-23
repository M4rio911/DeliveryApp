using DeliveryApp.Application.Dto.Payments;
using DeliveryApp.Application.Dto.Transportations;
using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Application.Interfaces.Repositories;
using DeliveryApp.Infrastructure.Models;
using DeliveryApp.Persistance;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DeliveryApp.Application.Handlers.Transportations.GetDriverDailyTransportations;

public class GetDriverDailyTransportationsHandler : IQueryHandler<GetDriverDailyTransportations, GetDriverDailyTransportationsResponse>
{
    private readonly DeliveryDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IDictionaryRepository _dictionaryRepository;

    public GetDriverDailyTransportationsHandler(DeliveryDbContext deliveryDbContext, IHttpContextAccessor httpContext, IDictionaryRepository dictionaryRepository)
    {
        _context = deliveryDbContext;
        _httpContextAccessor = httpContext;
        _dictionaryRepository = dictionaryRepository;
    }

    public async Task<GetDriverDailyTransportationsResponse> Handle(GetDriverDailyTransportations request, CancellationToken cancellationToken)
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
            return new GetDriverDailyTransportationsResponse("Driver was not found");
        }

        var transportation = await _context.Transportations
            .Where(x =>
                x.Id == request.TransportationId &&
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

        var collectedStatus = (await _dictionaryRepository.GetDictionary(
            DictionaryTypeEnum.PackageStatus.ToString(),
            PackageStatusEnum.Collected.ToString())).Id;

        var inssuedToDeliveryStatus = (await _dictionaryRepository.GetDictionary(
            DictionaryTypeEnum.PackageStatus.ToString(),
            PackageStatusEnum.IssuedToDelivery.ToString())).Id;

        var deliveriedStatus = (await _dictionaryRepository.GetDictionary(
            DictionaryTypeEnum.PackageStatus.ToString(),
            PackageStatusEnum.Delivered.ToString())).Id;

        var collectList = response.Where(x => x.PackageStatusId == toCollectStatus || x.PackageStatusId == collectedStatus).ToList();
        var deliverList = response.Where(x => x.PackageStatusId == inssuedToDeliveryStatus || x.PackageStatusId == deliveriedStatus).ToList();

        var transportationResponse = new GetDriverTransportationsDto()
        {
            PackagesToCollect = collectList,
            PackagesToDelivery = deliverList,
            TransportationStatus = transportation?.TransportationStatusId ?? 0,
            TransportationId = transportId,
            DateOfTransport = transportation?.DateOfTransport ?? DateTime.MinValue,
        };

        return new GetDriverDailyTransportationsResponse()
        {
            Transportation = transportationResponse
        };
    }
}
