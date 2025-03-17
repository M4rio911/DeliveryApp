using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Application.Interfaces.Repositories;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Persistance;
using DeliveryApp.Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using DeliveryApp.Application.Handlers.Addresses.EditAddress;

namespace DeliveryApp.Application.Handlers.Transportations.AssignPackageToTransportation;

public class AssignPackageToTransportationHandler : ICommandHandler<AssignPackageToTransportation, AssignPackageToTransportationResponse>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IDictionaryRepository _dictionaryRepository;
    private readonly DeliveryDbContext _context;

    public AssignPackageToTransportationHandler(IDictionaryRepository dictionaryRepository, IHttpContextAccessor httpContextAccessor, DeliveryDbContext deliveryDbContext)
    {
        _dictionaryRepository = dictionaryRepository;
        _httpContextAccessor = httpContextAccessor;
        _context = deliveryDbContext;
    }

    public async Task<AssignPackageToTransportationResponse> Handle(AssignPackageToTransportation request, CancellationToken cancellationToken)
    {
        var user = _httpContextAccessor.HttpContext?.User.Identities.FirstOrDefault().Name;
        if (user == null)
        {
            throw new UnauthorizedAccessException("User is not authenticated");
        }

        var packageDb = _context.Packages.Where(x => x.Id == request.PackageId).FirstOrDefault();
        if (packageDb == null)
            return new AssignPackageToTransportationResponse("No package was found under passed ID");

        var transportation = _context.Transportations.FirstOrDefault(x =>
            x.DateOfTransport.Date == request.TransportDate.Date &&
            x.AssignedDriverId == request.DriverId);

        if (transportation == null)
        {
            var scheduledStatus = (await _dictionaryRepository
                .GetDictionary(
                    DictionaryTypeEnum.TransportationStatus.ToString(),
                    TransportationStatusEnum.Scheduled.ToString()))
                .Id;

            transportation = new Transportation()
            {
                AssignedDriverId = request.DriverId,
                DateOfTransport = request.TransportDate.Date,
                Created = DateTime.UtcNow,
                CreatedBy = user,
                TransportationStatusId = scheduledStatus
            };

            _context.Transportations.Add(transportation);
            _context.SaveChanges();
        }

        var newTransportationItem = new TransportationItem()
        {
            PackageId = request.PackageId,
            TransportationId = transportation.Id,
            Created = DateTime.UtcNow,
            CreatedBy = user
        };

        _context.TransportationItems.Add(newTransportationItem);
        _context.SaveChanges();

        return new AssignPackageToTransportationResponse();
    }
}
