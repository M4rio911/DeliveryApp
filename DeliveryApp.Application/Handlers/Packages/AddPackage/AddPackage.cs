using DeliveryApp.Application.Handlers.Addresses.AddAddress;
using static DeliveryApp.Application.Interfaces.Mediator.ICommand;

namespace DeliveryApp.Application.Handlers.Packages.AddPackage;

public class AddPackage : ICommand<AddPackageResponse>
{
    public string ReciverEmail { get; set; }
    public int? DestinationId { get; set; }
    public int PackageTypeId { get; set; }
    public int PaymentTypeId { get; set; }
    public int CurrencyId { get; set; }
    public decimal Price { get; set; }
    public AddAddressParameters? GuestAddress { get; set; }


    public AddPackage(AddPackageParamenters parameters)
    {
        ReciverEmail = parameters.ReciverEmail;
        DestinationId = parameters.DestinationId;
        PackageTypeId = parameters.PackageTypeId;
        PaymentTypeId = parameters.PaymentTypeId;
        GuestAddress = parameters.GuestAddress;
        CurrencyId = parameters.CurrencyId;
        Price = parameters.Price;
    }
}
