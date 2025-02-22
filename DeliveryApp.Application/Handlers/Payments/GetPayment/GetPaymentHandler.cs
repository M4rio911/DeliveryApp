using DeliveryApp.Application.Dto.Payments;
using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Application.Interfaces.Repositories;
using DeliveryApp.Persistance;
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
                PaymentTypeId = x.PaymentTypeId,
                PaymentStatusId = x.PaymentStatusId,
                CurrencyId = x.CurrencyId,
                Price = x.Price
            }).FirstOrDefaultAsync(cancellationToken);

        if (response == null)
            return new GetPaymentResponse("No payment was found under passed Id");

        return new GetPaymentResponse()
        {
            Payment = response
        };
    }
}
