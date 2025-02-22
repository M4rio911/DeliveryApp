using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Application.Interfaces.Repositories;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Persistance;
using DeliveryApp.Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace DeliveryApp.Application.Handlers.Packages.AddPackage;

public class AddPackageHandler : ICommandHandler<AddPackage, AddPackageResponse>
{
    private readonly IDictionaryRepository _dictionaryRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IUserRepository _userRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly DeliveryDbContext _context;

    public AddPackageHandler(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository, IAddressRepository addressRepository, IDictionaryRepository dictionaryRepository, IPaymentRepository paymentRepository, DeliveryDbContext deliveryDbContext)
    {
        _dictionaryRepository = dictionaryRepository;
        _httpContextAccessor = httpContextAccessor;
        _paymentRepository = paymentRepository;
        _addressRepository = addressRepository;
        _userRepository = userRepository;
        _context = deliveryDbContext;
    }

    public async Task<AddPackageResponse> Handle(AddPackage request, CancellationToken cancellationToken)
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null)
        {
            throw new UnauthorizedAccessException("User is not authenticated");
        }
        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userName = user.Identities.FirstOrDefault().Name;

        var reciver = await _userRepository.GetUserByEmailNTAsync(request.ReciverEmail);
        if (reciver == null)
        {
            throw new Exception("Reciver not found");
        }

        if (request.DestinationId == null)
        {
            if(request.GuestAddress ==  null)
                throw new Exception("No address was passed");

            request.DestinationId = (await _addressRepository.AddGuestAddress(request.GuestAddress, userName)).Id;
        }

        var paymentTypeId = (await _dictionaryRepository.GetDictionary(DictionaryTypeEnum.PaymentType.ToString(), request.PaymentTypeId)).Id;
        var unpaidStatusId = (await _dictionaryRepository.GetDictionary(DictionaryTypeEnum.PaymentStatus.ToString(), PaymentStatusEnum.Unpaid.ToString())).Id;
        var newStatusId = (await _dictionaryRepository.GetDictionary(DictionaryTypeEnum.PackageStatus.ToString(), PackageStatusEnum.New.ToString())).Id;

        var payment = new Payment()
        {
            PaymentStatusId = unpaidStatusId,
            PaymentTypeId = paymentTypeId,
            CurrencyId = request.CurrencyId,
            Price = request.Price,
        };

        var paymentDb = _paymentRepository.AddPaymentAsync(payment);
        if (paymentDb == null)
        {
            throw new Exception("Error with add payment");
        }
        await _context.SaveChangesAsync(cancellationToken);

        var newPackage = new Package
        {
            SenderId = userId,
            ReciverId = reciver.Id,
            DestinationId = (int)request.DestinationId,
            PackageStatusId = newStatusId,
            PackageTypeId = request.PackageTypeId,
            PaymentId = paymentDb.Result.Id,
            ModifiedBy = userName,
            CreatedBy = userName,
            Created = DateTime.UtcNow
        };

        _context.Packages.Add(newPackage);
        await _context.SaveChangesAsync(cancellationToken);

        return new AddPackageResponse() 
        { 
            NewPackageId = newPackage.Id
        };
    }
}
