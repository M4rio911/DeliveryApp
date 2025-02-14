using DeliveryApp.Application.Dto.Payments;
using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Application.Interfaces.Repositories;
using DeliveryApp.Persistance;
using DeliveryApp.Persistance.Models;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.Payments.GetPayment;

public class GetPaymentHandler : IQueryHandler<GetPayment, GetPaymentResponse>
{
    private readonly DeliveryDbContext _context;
    private readonly IDictionaryRepository _dictionaryRepository;

    public GetPaymentHandler(DeliveryDbContext deliveryDbContext, IDictionaryRepository dictionaryRepository)
    {
        _context = deliveryDbContext;
        _dictionaryRepository = dictionaryRepository;
    }

    public async Task<GetPaymentResponse> Handle(GetPayment request, CancellationToken cancellationToken)
    {
        var response = await _context.Payments
            .Where(x => x.Id == request.PaymentId)
            .Select(x => new GetPaymentDto()
            {
                Id = x.Id,
                PackageId = x.Id,
                PaymentTypeId = x.PaymentTypeId,
                PaymentStatusId = x.PaymentStatusId,
            }).FirstOrDefaultAsync(cancellationToken);

        if (response == null)
            return new GetPaymentResponse("No payment was found under passed Id");

        response.PaymentStatus = (await _dictionaryRepository.GetByIdNTAsync(response.PaymentStatusId)).Name;
        response.PaymentType = (await _dictionaryRepository.GetByIdNTAsync(response.PaymentTypeId)).Name;

        return new GetPaymentResponse()
        {
            Payment = response
        };
    }
}
