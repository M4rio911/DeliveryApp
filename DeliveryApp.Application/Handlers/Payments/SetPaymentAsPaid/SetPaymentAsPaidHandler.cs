using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Application.Interfaces.Repositories;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Persistance;
using DeliveryApp.Persistance.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.Payments.SetPaymentAsPaid;

public class SetPaymentAsPaidHandler : ICommandHandler<SetPaymentAsPaid, SetPaymentAsPaidResponse>
{
    private readonly IDictionaryRepository _dictionaryRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly DeliveryDbContext _context;

    public SetPaymentAsPaidHandler(IDictionaryRepository dictionaryRepository, IHttpContextAccessor httpContextAccessor, DeliveryDbContext deliveryDbContext)
    {
        _dictionaryRepository = dictionaryRepository;
        _httpContextAccessor = httpContextAccessor;
        _context = deliveryDbContext;
    }

    public async Task<SetPaymentAsPaidResponse> Handle(SetPaymentAsPaid request, CancellationToken cancellationToken)
    {
        var user = _httpContextAccessor.HttpContext?.User.Identities.FirstOrDefault().Name;
        if (user == null)
        {
            throw new UnauthorizedAccessException("User is not authenticated");
        }

        var paymentPaidStatus = (await _dictionaryRepository.GetDictionary(DictionaryTypeEnum.PaymentStatus.ToString(), PaymentStatusEnum.Paid.ToString())).Id;
        var packagePaidStatus = (await _dictionaryRepository.GetDictionary(DictionaryTypeEnum.PackageStatus.ToString(), PackageStatusEnum.Paid.ToString())).Id;

        //PAYMENT
        var dbPayment = await _context.Payments
            .Where(x => x.Id == request.PaymentId)
            .FirstOrDefaultAsync(cancellationToken);
            
        if(dbPayment == null) 
            return new SetPaymentAsPaidResponse("No Payment was found under passed ID");

        dbPayment.PaymentStatusId = paymentPaidStatus;
        dbPayment.Modified = DateTime.UtcNow;
        dbPayment.ModifiedBy = user;

        //PACKAGE
        var dbPackage = await _context.Packages
            .Where(x => x.PaymentId == request.PaymentId)
            .FirstOrDefaultAsync(cancellationToken);

        if (dbPackage == null)
            return new SetPaymentAsPaidResponse("No Package was found under passed ID");

        dbPackage.PackageStatusId = packagePaidStatus;
        dbPackage.Modified = DateTime.UtcNow;
        dbPackage.ModifiedBy = user;

        _context.Update(dbPayment);
        _context.Update(dbPackage);
        await _context.SaveChangesAsync(cancellationToken);

        return new SetPaymentAsPaidResponse();
    }
}
