using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Application.Interfaces.Repositories;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Persistance;
using DeliveryApp.Persistance.Models;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.Payments.SetPaymentAsPaid;

public class SetPaymentAsPaidHandler : ICommandHandler<SetPaymentAsPaid, SetPaymentAsPaidResponse>
{
    private readonly DeliveryDbContext _context;
    private readonly IDictionaryRepository _dictionaryRepository;

    public SetPaymentAsPaidHandler(DeliveryDbContext deliveryDbContext, IDictionaryRepository dictionaryRepository)
    {
        _context = deliveryDbContext;
        _dictionaryRepository = dictionaryRepository;
    }

    public async Task<SetPaymentAsPaidResponse> Handle(SetPaymentAsPaid request, CancellationToken cancellationToken)
    {
        var dbPayment = await _context.Payments
            .Where(x => x.Id == request.PaymentId)
            .FirstOrDefaultAsync(cancellationToken);
            
        if(dbPayment == null) 
            return new SetPaymentAsPaidResponse("No Payment was found under passed ID");

        var paidStatus = (await _dictionaryRepository.GetDictionary(DictionaryTypeEnum.PaymentStatus.ToString(), PaymentStatusEnum.Paid.ToString())).Id;

        dbPayment.PaymentStatusId = paidStatus;
        dbPayment.ModifiedBy = "ModifiedUser";

        _context.Update(dbPayment);
        await _context.SaveChangesAsync(cancellationToken);

        return new SetPaymentAsPaidResponse();
    }
}
