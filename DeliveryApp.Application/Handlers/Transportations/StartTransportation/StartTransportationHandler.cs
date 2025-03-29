using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Application.Interfaces.Repositories;
using DeliveryApp.Infrastructure.Models;
using DeliveryApp.Persistance;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DeliveryApp.Application.Handlers.Transportations.StartTransportation;

public class StartTransportationHandler : IQueryHandler<StartTransportation, StartTransportationResponse>
{
    private readonly DeliveryDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IDictionaryRepository _dictionaryRepository;

    public StartTransportationHandler(DeliveryDbContext deliveryDbContext, IHttpContextAccessor httpContext, IDictionaryRepository dictionaryRepository)
    {
        _context = deliveryDbContext;
        _httpContextAccessor = httpContext;
        _dictionaryRepository = dictionaryRepository;
    }

    public async Task<StartTransportationResponse> Handle(StartTransportation request, CancellationToken cancellationToken)
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
            return new StartTransportationResponse("Driver was not found");
        }

        var transportation = await _context.Transportations
            .Where(x =>
                x.DateOfTransport.Date == request.SelectedDate.Date &&
                x.AssignedDriverId == driver.Id)
            .FirstOrDefaultAsync();

        if (transportation == null)
        {
            return new StartTransportationResponse("Transportation was not found");
        }

        var startedTransportationStatus = (await _dictionaryRepository.GetDictionary(
            DictionaryTypeEnum.TransportationStatus.ToString(),
            TransportationStatusEnum.Started.ToString())).Id;

        var assignedToDeliveryStatus = (await _dictionaryRepository.GetDictionary(
            DictionaryTypeEnum.PackageStatus.ToString(),
            PackageStatusEnum.AssignedToDelivery.ToString())).Id;

        var issuedToDeliveryStatus = (await _dictionaryRepository.GetDictionary(
            DictionaryTypeEnum.PackageStatus.ToString(),
            PackageStatusEnum.IssuedToDelivery.ToString())).Id;

        var assignedToDeliveryList = _context.TransportationItems
            .Include(x => x.Package)
            .Where(x => x.TransportationId == transportation.Id &&
                        x.Package.PackageStatusId == assignedToDeliveryStatus);


        transportation.TransportationStatusId = startedTransportationStatus;

        foreach (var item in assignedToDeliveryList)
        {
            item.Package.PackageStatusId = issuedToDeliveryStatus;
        }

        var issuedPackagesIds = assignedToDeliveryList.Select(x => x.Package.Id).ToList();

        var storagePackages = _context.StoragePackages.Where(x => issuedPackagesIds.Contains(x.Package.Id)).ToList();

        foreach (var package in storagePackages) 
        { 
            package.DateOfExit = DateTime.UtcNow;
        }

        _context.SaveChanges();

        return new StartTransportationResponse();
    }
}
