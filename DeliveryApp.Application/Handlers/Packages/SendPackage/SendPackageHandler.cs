using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Application.Interfaces.Repositories;
using DeliveryApp.Infrastructure.Models;
using DeliveryApp.Infrastructure.Strategies;
using DeliveryApp.Persistance;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.Packages.SendPackage;

public class SendPackageHandler : ICommandHandler<SendPackage, SendPackageResponse>
{
    private readonly IDictionaryRepository _dictionaryRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly DeliveryDbContext _context;

    public SendPackageHandler(IDictionaryRepository dictionaryRepository, IHttpContextAccessor httpContextAccessor, DeliveryDbContext deliveryDbContext)
    {
        _dictionaryRepository = dictionaryRepository;
        _httpContextAccessor = httpContextAccessor;
        _context = deliveryDbContext;
    }

    public async Task<SendPackageResponse> Handle(SendPackage request, CancellationToken cancellationToken)
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
            return new SendPackageResponse("No Package was found under passed ID");

        var currentPackageStatus = (PackageStatusEnum)Enum.Parse(typeof(PackageStatusEnum),
            (await _dictionaryRepository.GetDictionary(DictionaryTypeEnum.PackageStatus.ToString(), dbPackage.PackageStatusId)).Name);

        var nextPackageStatus = ChangePackageStatusStretegy.GetNextPackageStatus(currentPackageStatus);
        var nextPackageStatusId = (await _dictionaryRepository.GetDictionary(DictionaryTypeEnum.PackageStatus.ToString(), nextPackageStatus.ToString())).Id;

        dbPackage.PackageStatusId = nextPackageStatusId;
        dbPackage.Modified = DateTime.UtcNow;
        dbPackage.ModifiedBy = user;

        _context.Update(dbPackage);
        await _context.SaveChangesAsync(cancellationToken);

        return new SendPackageResponse();
    }
}
