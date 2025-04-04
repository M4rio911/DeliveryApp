using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Application.Interfaces.Repositories;
using DeliveryApp.Infrastructure.Models;
using DeliveryApp.Infrastructure.Strategies;
using DeliveryApp.Persistance;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.Packages.CollectPackage;

public class CollectPackageHandler : ICommandHandler<CollectPackage, CollectPackageResponse>
{
    private readonly IDictionaryRepository _dictionaryRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly DeliveryDbContext _context;

    public CollectPackageHandler(IDictionaryRepository dictionaryRepository, IHttpContextAccessor httpContextAccessor, DeliveryDbContext deliveryDbContext)
    {
        _dictionaryRepository = dictionaryRepository;
        _httpContextAccessor = httpContextAccessor;
        _context = deliveryDbContext;
    }

    public async Task<CollectPackageResponse> Handle(CollectPackage request, CancellationToken cancellationToken)
    {
        var user = _httpContextAccessor.HttpContext?.User.Identities.FirstOrDefault().Name;
        if (user == null)
        {
            throw new UnauthorizedAccessException("User is not authenticated");
        }

        //PACKAGE
        var dbPackage = await _context.Packages
            .Where(x => x.Id == request.PackageId)
            .FirstOrDefaultAsync(cancellationToken);

        if (dbPackage == null)
            return new CollectPackageResponse("No Package was found under passed ID");

        var currentPackageStatus = (PackageStatusEnum)Enum.Parse(typeof(PackageStatusEnum),
            (await _dictionaryRepository.GetDictionary(DictionaryTypeEnum.PackageStatus.ToString(), dbPackage.PackageStatusId)).Name);

        var nextPackageStatus = ChangePackageStatusStretegy.GetNextPackageStatus(currentPackageStatus);
        var nextPackageStatusId = (await _dictionaryRepository.GetDictionary(DictionaryTypeEnum.PackageStatus.ToString(), nextPackageStatus.ToString())).Id;

        dbPackage.PackageStatusId = nextPackageStatusId;
        dbPackage.Modified = DateTime.UtcNow;
        dbPackage.ModifiedBy = user;

        _context.Update(dbPackage);
        await _context.SaveChangesAsync(cancellationToken);

        return new CollectPackageResponse();
    }
}
