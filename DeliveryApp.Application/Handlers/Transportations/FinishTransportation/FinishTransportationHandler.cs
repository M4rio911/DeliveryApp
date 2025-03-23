using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Application.Interfaces.Repositories;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Infrastructure.Models;
using DeliveryApp.Persistance;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DeliveryApp.Application.Handlers.Transportations.FinishTransportation;

public class FinishTransportationHandler : IQueryHandler<FinishTransportation, FinishTransportationResponse>
{
    private readonly DeliveryDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IDictionaryRepository _dictionaryRepository;

    public FinishTransportationHandler(DeliveryDbContext deliveryDbContext, IHttpContextAccessor httpContext, IDictionaryRepository dictionaryRepository)
    {
        _context = deliveryDbContext;
        _httpContextAccessor = httpContext;
        _dictionaryRepository = dictionaryRepository;
    }

    public async Task<FinishTransportationResponse> Handle(FinishTransportation request, CancellationToken cancellationToken)
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
            return new FinishTransportationResponse("Driver was not found");
        }

        var transportation = await _context.Transportations
            .Where(x =>
                x.Id == request.TransportationId &&
                x.AssignedDriverId == driver.Id)
            .FirstOrDefaultAsync();

        if (transportation == null)
        {
            return new FinishTransportationResponse("Transportation was not found");
        }

        var finishedTransportationStatus = (await _dictionaryRepository.GetDictionary(
            DictionaryTypeEnum.TransportationStatus.ToString(),
            TransportationStatusEnum.Finished.ToString())).Id;

        transportation.TransportationStatusId = finishedTransportationStatus;

        var collectedStatus = (await _dictionaryRepository.GetDictionary(
            DictionaryTypeEnum.PackageStatus.ToString(),
            PackageStatusEnum.Collected.ToString())).Id;

        var inStorageStatus = (await _dictionaryRepository.GetDictionary(
            DictionaryTypeEnum.PackageStatus.ToString(),
            PackageStatusEnum.Storage.ToString())).Id;

        //STORAGE

        var assignedToDeliveryList = _context.TransportationItems
            .Include(x => x.Package)
            .Where(x => x.TransportationId == transportation.Id &&
                        x.Package.PackageStatusId == collectedStatus);

        foreach (var item in assignedToDeliveryList)
        {
            item.Package.PackageStatusId = inStorageStatus;

            var newStorageItem = new StoragePackages()
            {
                Created = DateTime.UtcNow,
                DateOfArrival = DateTime.UtcNow,
                CreatedBy = user.Name,
                PackageId = item.Id
            };

            _context.Add(newStorageItem);
        }

        _context.SaveChanges();

        return new FinishTransportationResponse();
    }
}
