using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Application.Interfaces.Repositories;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Persistance;
using DeliveryApp.Persistance.Models;
using Microsoft.AspNetCore.Http;

namespace DeliveryApp.Application.Handlers.Payments.AddPayment;

public class AddPaymentHandler : ICommandHandler<AddPayment, AddPaymentResponse>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IDictionaryRepository _dictionaryRepository;
    private readonly DeliveryDbContext _context;

    public AddPaymentHandler(IDictionaryRepository dictionaryRepository, IHttpContextAccessor httpContextAccessor, DeliveryDbContext deliveryDbContext)
    {
        _dictionaryRepository = dictionaryRepository;
        _httpContextAccessor = httpContextAccessor;
        _context = deliveryDbContext;
    }

    public async Task<AddPaymentResponse> Handle(AddPayment request, CancellationToken cancellationToken)
    {
        var unpaidStatus = (await _dictionaryRepository.GetDefaultDictionaryNTAsync(DictionaryTypeEnum.PaymentStatus.ToString())).Id;

        var user = _httpContextAccessor.HttpContext?.User.Identities.FirstOrDefault().Name;
        if (user == null)
        {
            throw new UnauthorizedAccessException("User is not authenticated");
        }

        var newPayment = new Payment
        {
            PackageId = request.PackageId,
            PaymentTypeId = request.PaymentTypeId,
            PaymentStatusId = unpaidStatus,
            Created = DateTime.UtcNow,
            CreatedBy = user,
            ModifiedBy = user
        };


        _context.Payments.Add(newPayment);
        await _context.SaveChangesAsync(cancellationToken);

        return new AddPaymentResponse();
    }
}
