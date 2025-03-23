using DeliveryApp.Application.Dto.Packages;
using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Persistance;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.Packages.GetPackageDetails;

public class GetPackageDetailsHandler : IQueryHandler<GetPackageDetails, GetPackageDetailsResponse>
{
    private readonly DeliveryDbContext _context;

    public GetPackageDetailsHandler(DeliveryDbContext deliveryDbContext)
    {
        _context = deliveryDbContext;
    }

    public async Task<GetPackageDetailsResponse> Handle(GetPackageDetails request, CancellationToken cancellationToken)
    {
        var response = await _context.Packages
            .Include(x => x.Sender)
            .Include(x => x.Reciver)
            .Include(x => x.Payment)
            .Include(x => x.Destination)
                .ThenInclude(x => x.Country)
            .Where(x => x.Id == request.PackageId)
            .Select(x => new GetDriverTransportationPackageDto() 
            {
                PackageId = x.Id,
                SenderEmail = x.Sender.Email,
                ReciverEmail = x.Reciver.Email,
                PackageStatusId = x.PackageStatusId,
                PackageTypeId = x.PackageTypeId,
                DestinationAddress = new Dto.Addresses.GetAddressDto
                {
                    AddressTypeId = x.Destination.AddressTypeId,
                    Country = x.Destination.Country.Name,
                    City = x.Destination.City,
                    PostCode = x.Destination.PostCode,
                    Street = x.Destination.Street,
                    Number = x.Destination.Number,
                    GuestAddress = x.Destination.GuestAddress
                },
                Payment = new Dto.Payments.GetPaymentDto
                {
                    CurrencyId = x.Payment.CurrencyId,
                    PaymentStatusId = x.Payment.PaymentStatusId,
                    PaymentTypeId = x.Payment.PaymentTypeId,
                    Price = x.Payment.Price,
                }

            }).FirstOrDefaultAsync(cancellationToken);

        if (response == null)
            return new GetPackageDetailsResponse("No package was found under passed Id");

        return new GetPackageDetailsResponse()
        {
            PackageDetails = response
        };
    }
}
