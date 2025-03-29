using DeliveryApp.Application.Dto.Packages;
using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Application.Interfaces.Repositories;
using DeliveryApp.Infrastructure.Models;
using DeliveryApp.Infrastructure.Strategies;
using DeliveryApp.Persistance;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DeliveryApp.Application.Handlers.Packages.GetUserPackages;

public class GetUserPackagesHandler : IQueryHandler<GetUserPackages, GetUserPackagesResponse>
{
    private readonly IDictionaryRepository _dictionaryRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly DeliveryDbContext _context;

    public GetUserPackagesHandler(IHttpContextAccessor httpContextAccessor, IDictionaryRepository dictionaryRepository, DeliveryDbContext deliveryDbContext)
    {
        _dictionaryRepository = dictionaryRepository;
        _httpContextAccessor = httpContextAccessor;
        _context = deliveryDbContext;
    }

    public async Task<GetUserPackagesResponse> Handle(GetUserPackages request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            throw new UnauthorizedAccessException("User is not authenticated");
        }
        var packageStatuses = (await _dictionaryRepository
            .GetDictionariesByType(DictionaryTypeEnum.PackageStatus.ToString()))
            .ToDictionary(x => x.Id, x => x.Name);

        var response = await _context.Packages
           .Include(x => x.Sender)
           .Include(x => x.Reciver)
           .Where(x => x.SenderId == userId)
           .Select(x => new GetPackageDto
           {
               PackageId = x.Id,
               SenderEmail = x.Sender.Email,
               ReciverEmail = x.Reciver.Email,
               PackageStatusId = GetPublicPackageStatusId(x.PackageStatusId, packageStatuses),
               PackageTypeId = x.PackageTypeId,
               PaymentId = x.PaymentId,
           })
           .ToListAsync(cancellationToken);

        return new GetUserPackagesResponse()
        {
            UserPackages = response
        };
    }
    private static int GetPublicPackageStatusId(int packageStatusId, Dictionary<int, string> packageStatuses)
    {
        if (!packageStatuses.TryGetValue(packageStatusId, out var statusName) ||
            !Enum.TryParse<PackageStatusEnum>(statusName, true, out var enumName))
        {
            return packageStatusId; // Brak konwersji, oryginalny ID
        }

        var publicStatusName = ChangePackageStatusStretegy.ConvertToPublicPackageStatus(enumName);
        if (publicStatusName == enumName)
        {
            return packageStatusId;
        }

        return packageStatuses.FirstOrDefault(x => x.Value == publicStatusName.ToString()).Key;
    }
}
