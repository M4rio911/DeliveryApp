﻿using DeliveryApp.Application.Dto.Payments;
using DeliveryApp.Application.Handlers.Payments.GetPayment;
using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Application.Interfaces.Repositories;
using DeliveryApp.Persistance;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.Payments.GetPaymentByPackageId;

public class GetPaymentByPackageIdHandler : IQueryHandler<GetPaymentByPackageId, GetPaymentByPackageIdResponse>
{
    private readonly DeliveryDbContext _context;
    private readonly IDictionaryRepository _dictionaryRepository;
    public GetPaymentByPackageIdHandler(DeliveryDbContext deliveryDbContext, IDictionaryRepository dictionaryRepository)
    {
        _context = deliveryDbContext;
        _dictionaryRepository = dictionaryRepository;
    }

    public async Task<GetPaymentByPackageIdResponse> Handle(GetPaymentByPackageId request, CancellationToken cancellationToken)
    {
        var response = await _context.Payments
            .Where(x => x.Id == request.PackageId)
            .Select(x => new GetPaymentDto()
            {
                Id = x.Id,
                PaymentTypeId = x.PaymentTypeId,
                PaymentStatusId = x.PaymentStatusId,
                CurrencyId = x.CurrencyId,
                Price = x.Price
            }).FirstOrDefaultAsync(cancellationToken);

        if (response == null)
            return new GetPaymentByPackageIdResponse("No payment was found under passed Id");

        return new GetPaymentByPackageIdResponse()
        {
            Payment = response
        };
    }
}
