using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Application.Interfaces.Repositories;
using DeliveryApp.Infrastructure.Models;
using DeliveryApp.Infrastructure.Strategies;
using DeliveryApp.Persistance;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.Packages.AssignPackage;

public class AssignPackageHandler : ICommandHandler<AssignPackage, AssignPackageResponse>
{
    private readonly IDictionaryRepository _dictionaryRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly DeliveryDbContext _context;

    public AssignPackageHandler(IDictionaryRepository dictionaryRepository, IHttpContextAccessor httpContextAccessor, DeliveryDbContext deliveryDbContext)
    {
        _dictionaryRepository = dictionaryRepository;
        _httpContextAccessor = httpContextAccessor;
        _context = deliveryDbContext;
    }

    public async Task<AssignPackageResponse> Handle(AssignPackage request, CancellationToken cancellationToken)
    {
        var user = _httpContextAccessor.HttpContext?.User.Identities.FirstOrDefault().Name;
        if (user == null)
        {
            throw new UnauthorizedAccessException("User is not authenticated");
        }

        var deliveryTypeId = (await _dictionaryRepository.GetDictionary(DictionaryTypeEnum.UserType.ToString(), UserTypeEnum.Delivery.ToString())).Id;
        var selectedDriver = _context.Drivers
            .Include(x => x.BaseUser)
            .Where(x => 
                x.Id == request.DriverId && 
                x.BaseUser.UserTypeId == deliveryTypeId &&
                x.BaseUser.ActiveStatus )
            .FirstOrDefault();

        if (selectedDriver == null)
            return new AssignPackageResponse("No driver was found under passed ID");

        //PACKAGE
        var dbPackage = await _context.Packages
            .Where(x => x.Id == request.PackageId)
            .FirstOrDefaultAsync(cancellationToken);

        if (dbPackage == null)
            return new AssignPackageResponse("No Package was found under passed ID");

        var currentPackageStatus = (PackageStatusEnum)Enum.Parse(typeof(PackageStatusEnum),
            (await _dictionaryRepository.GetDictionary(DictionaryTypeEnum.PackageStatus.ToString(), dbPackage.PackageStatusId)).Name);

        if(currentPackageStatus != PackageStatusEnum.Posted && 
            currentPackageStatus != PackageStatusEnum.Storage)
        {
            return new AssignPackageResponse("Package cant be assigned to delivery");
        }

        var nextPackageStatus = ChangePackageStatusStretegy.GetNextPackageStatus(currentPackageStatus);
        var nextPackageStatusId = (await _dictionaryRepository.GetDictionary(DictionaryTypeEnum.PackageStatus.ToString(), nextPackageStatus.ToString())).Id;

        var scheduledTransportId = (await _dictionaryRepository.GetDictionary(DictionaryTypeEnum.TransportationStatus.ToString(), TransportationStatusEnum.Scheduled.ToString())).Id;

        
        //TRANSPORTATION
        var transportation = _context.Transportations.FirstOrDefault(x =>
            x.AssignedDriverId == selectedDriver.Id &&
            x.DateOfTransport == request.TransportDate);
        if (transportation == null)
        {
            transportation = new Domain.Entities.Transportation()
            {
                Created = DateTime.UtcNow,
                CreatedBy = user,
                AssignedDriverId = selectedDriver.Id,
                DateOfTransport = request.TransportDate.Date,
                TransportationStatusId = scheduledTransportId,
            };
            _context.Transportations.Add(transportation);
            _context.SaveChanges();
        }

        //TRANSPORTATION ITEM 
        var transportationItem = new Domain.Entities.TransportationItem()
        {
            Created = DateTime.UtcNow,
            CreatedBy = user,
            PackageId = dbPackage.Id,
            TransportationId = transportation.Id
        };
        _context.Add(transportationItem);
        _context.SaveChanges();

        dbPackage.PackageStatusId = nextPackageStatusId;
        dbPackage.Modified = DateTime.UtcNow;
        dbPackage.ModifiedBy = user;

        _context.Update(dbPackage);
        await _context.SaveChangesAsync(cancellationToken);

        return new AssignPackageResponse();
    }
}
