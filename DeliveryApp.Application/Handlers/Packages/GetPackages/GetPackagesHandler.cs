using DeliveryApp.Application.Dto.Packages;
using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Application.Interfaces.Repositories;
using DeliveryApp.Infrastructure.Models;
using DeliveryApp.Persistance;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.Packages.GetPackages;

public class GetPackagesHandler : IQueryHandler<GetPackages, GetPackagesResponse>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IDictionaryRepository _dictionaryRepository;
    private readonly DeliveryDbContext _context;

    public GetPackagesHandler(IHttpContextAccessor httpContextAccessor, IDictionaryRepository dictionaryRepository,  DeliveryDbContext deliveryDbContext)
    {
        _httpContextAccessor = httpContextAccessor;
        _dictionaryRepository = dictionaryRepository;
        _context = deliveryDbContext;
    }

    public async Task<GetPackagesResponse> Handle(GetPackages request, CancellationToken cancellationToken)
    {
        var inStorage = (await _dictionaryRepository.GetDictionary(
            DictionaryTypeEnum.PackageStatus.ToString(),
            PackageStatusEnum.Storage.ToString())).Id;

        var response = await _context.Packages
            .Where(x => request.PackageStatusId == 0 || x.PackageStatusId == request.PackageStatusId)
            .Include(x => x.Sender)
            .Include(x => x.Reciver)
            .Select(x => new GetPackageDto() 
            {
                PackageId = x.Id,
                SenderEmail = x.Sender.Email,
                ReciverEmail = x.Reciver.Email,
                PackageStatusId = x.PackageStatusId,
                PackageTypeId = x.PackageTypeId,
                PaymentId = x.PaymentId,
                ArrivalDate = null,
            }).ToListAsync(cancellationToken);



        //STORAGE
        var packageIds = response.Select(x => x.PackageId).ToList();

        if (request.PackageStatusId == inStorage && packageIds.Any())
        {
            var storagePackages = await _context.StoragePackages
                .Where(x => packageIds.Contains(x.PackageId))
                .ToListAsync(cancellationToken);

            foreach (var package in response)
            {
                var storagePackage = storagePackages.FirstOrDefault(x => x.PackageId == package.PackageId);
                if (storagePackage != null)
                {
                    package.ArrivalDate = storagePackage.DateOfArrival;
                }
            }

            response = response.OrderBy(x => x.ArrivalDate ?? DateTime.MaxValue).ToList();
        }

        return new GetPackagesResponse()
        {
            Packages = response
        };
    }
}
