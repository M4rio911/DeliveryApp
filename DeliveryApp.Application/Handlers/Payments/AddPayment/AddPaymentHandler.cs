using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Application.Interfaces.Repositories;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Persistance;
using DeliveryApp.Persistance.Models;

namespace DeliveryApp.Application.Handlers.Payments.AddPayment;

public class AddPaymentHandler : ICommandHandler<AddPayment, AddPaymentResponse>
{
    private readonly DeliveryDbContext _context;
    private readonly IDictionaryRepository _dictionaryRepository;

    public AddPaymentHandler(DeliveryDbContext deliveryDbContext, IDictionaryRepository dictionaryRepository)
    {
        _context = deliveryDbContext;
        _dictionaryRepository = dictionaryRepository;
    }

    public async Task<AddPaymentResponse> Handle(AddPayment request, CancellationToken cancellationToken)
    {
        var unpaidStatus = (await _dictionaryRepository.GetDefaultDictionaryNTAsync(DictionaryTypeEnum.PaymentStatus.ToString())).Id;

        var newPayment = new Payment
        {
            PackageId = request.PackageId,
            PaymentTypeId = request.PaymentTypeId,
            PaymentStatusId = unpaidStatus,
            Created = DateTime.UtcNow,
            CreatedBy = "testUser",
            ModifiedBy = "testUser"
        };


        _context.Payments.Add(newPayment);
        await _context.SaveChangesAsync(cancellationToken);

        return new AddPaymentResponse();
    }
}
